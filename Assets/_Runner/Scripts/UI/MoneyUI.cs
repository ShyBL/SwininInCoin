using TMPro;
using UnityEngine;

public class MoneyUI : MyMonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MoneyText;
    void Start()
    {
        MoneyText.text = GameManager.PlayerManager.Money.ToString();
    }
    
    void Update()
    {
        MoneyText.text = GameManager.PlayerManager.Money.ToString();
    }
}