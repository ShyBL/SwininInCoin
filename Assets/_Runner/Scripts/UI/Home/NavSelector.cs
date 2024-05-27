using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NavSelector : MyMonoBehaviour
{
    [SerializeField] private List<GameObject> selections;
    [SerializeField] private GameObject selection;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    private int current;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = selection.transform.position;
        InitializeSelections();

        leftButton.onClick.AddListener(ScrollLeft);
        rightButton.onClick.AddListener(ScrollRight);
    }

    private void InitializeSelections()
    {
        for (int i = 0; i < selections.Count; i++)
        {
            selections[i].SetActive(i == current);
        }
    }

    private void ScrollLeft()
    {
        if (selections.Count == 0) return;

        selections[current].SetActive(false);
        current = (current - 1 + selections.Count) % selections.Count;
        selections[current].SetActive(true);

        Vector3 newPosition = initialPosition + new Vector3(-27 * current, 0, 0);
        selection.transform.DOMove(newPosition, 0.5f);
    }

    private void ScrollRight()
    {
        if (selections.Count == 0) return;

        selections[current].SetActive(false);
        current = (current + 1) % selections.Count;
        selections[current].SetActive(true);

        Vector3 newPosition = initialPosition + new Vector3(-27 * current, 0, 0);
        selection.transform.DOMove(newPosition, 0.5f);
    }
}