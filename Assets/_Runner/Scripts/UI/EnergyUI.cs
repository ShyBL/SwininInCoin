using TMPro;
using UnityEngine;

public class EnergyUI : MyMonoBehaviour
{
    [SerializeField] private TextMeshProUGUI EnergyText;
    void Start()
    {
        EnergyText.text = GameManager.PlayerManager.Energy.ToString();
    }
    
    void Update()
    {
        EnergyText.text = GameManager.PlayerManager.Energy.ToString();
    }
}