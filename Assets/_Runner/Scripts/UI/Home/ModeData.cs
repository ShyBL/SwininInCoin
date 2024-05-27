using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModeData : MyMonoBehaviour
{
    public bool locked;
    public string name;
    public string description;

    [SerializeField] private ShipType type;
    
    [SerializeField] private Button confirmButton;
    [SerializeField] private GameObject confirmIcon, lockIcon;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI buttonText;

    private void Start()
    {
        buttonText.text = name;
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
        confirmIcon.SetActive(true);
        GameManager.PlayerManager.CurrentShip = type;
    }
}

public enum ShipType
{
    Tutorial, Cadet, Pirate, Privateer
}