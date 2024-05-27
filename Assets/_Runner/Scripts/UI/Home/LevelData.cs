using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelData : MyMonoBehaviour
{
    public bool locked;
    public string description;
    
    [SerializeField] private Button confirmButton;
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private TextMeshProUGUI descriptionText;
    private void Start()
    {
        descriptionText.text = description;
        if (locked)
        {
            confirmButton.interactable = false;
            lockIcon.SetActive(true);
        }
        confirmButton.onClick.AddListener(ConfirmIconAction);
    }

    void ConfirmIconAction()
    {
        SceneManager.LoadScene("Main");
    }
}