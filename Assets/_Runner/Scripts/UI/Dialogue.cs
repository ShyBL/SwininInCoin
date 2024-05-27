using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private RectTransform bubbleArrow;
    [SerializeField] private TextMeshProUGUI textUI;
    private IEnumerator displayInst;
    private bool hurry = false;
    public float defaultDelayBetweenLetters = 0.1f;
    public float timePadding = 3.0f;
    public bool typing = false;
        
    private Player playerMove;
    public void Initialize(string textToDisplay, float timeBetweenLetters, Player player)
    {
        playerMove = player;
        gameObject.SetActive(true);
        var offset = new Vector3(-130, -130);
        bubbleArrow.localPosition = offset;

        DisplaySubtitle(textToDisplay, timeBetweenLetters);
    }

    private void DisplaySubtitle(string sub, float displayFor = -1, bool hideOnFinish = false) 
    {
        //if there's something already being displayed, stop it and replace it
        if (displayInst != null) 
        {
            StopCoroutine(displayInst);
            textUI.text = "";
            displayInst = null;
        }
        hurry = false; 
        displayInst = DisplayTyping(sub, displayFor, hideOnFinish);
        StartCoroutine(displayInst);
    }
    
    IEnumerator DisplayTyping(string sub, float displayFor, bool hideOnFinish) 
    {
        float timer = 0;
        hurry = false;
        textUI.text = "";
        float timeBetweenLetters = (displayFor + .001f) / ((float)sub.Length);
        float bonusPadding = 0; //any displayFor time that's left after typing has finished gets added to the timePadding

        if (timeBetweenLetters > defaultDelayBetweenLetters) {
            timeBetweenLetters = defaultDelayBetweenLetters;
            bonusPadding = displayFor - timeBetweenLetters * sub.Length;
            if (bonusPadding < 0)//for fringe cases
                bonusPadding = 0;
        }

        while (sub.Length > 0 && !hurry) {
            timer += Time.deltaTime;
            float onDis = Mathf.Round(timer / timeBetweenLetters);
            onDis -= textUI.text.Length;
            for (int i = 0; i < onDis; i++) {
                textUI.text += sub[0];
                sub = sub.Remove(0, 1);
                if (sub.Length <= 0)
                    break;
            }
            yield return null;
        }

        //if hurried, post the rest of the phrase immediately and set scroll to max
        if (sub.Length > 0)
            textUI.text += sub;
        yield return null;//give UI time to update before fixing scroll position

        //use the time padding for finishing reading
        if (timePadding + bonusPadding > 0)
            yield return new WaitForSeconds(timePadding + bonusPadding);
        if (hideOnFinish)
            DisplayCleanup();
    }
    
    public void DisplayCleanup() 
    {
        textUI.text = "";
        displayInst = null;
        gameObject.SetActive(false);
        playerMove.ReleaseFromStop();
        // if (playerMove.isStoppingForward && playerMove.forwardSpeed <= 0)
        // {
        //     playerMove.ReleaseFromStop();
        // }
    }

    private void Hurry() 
    {
        hurry = true;
    }

    private void Stop() 
    {
        if (displayInst != null)
            StopCoroutine(displayInst);
        displayInst = null;
        DisplayCleanup();
    }

    // private void Update()
    // {
    //     if (Input.touchCount > 0)
    //     {
    //         if (!hurry)
    //         {
    //             Hurry();
    //         }
    //         else
    //         {
    //             Stop();
    //         }
    //     }
    // }
}
