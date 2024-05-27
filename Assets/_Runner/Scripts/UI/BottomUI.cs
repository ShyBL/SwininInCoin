using DG.Tweening;
using TMPro;
using UnityEngine;
using ER.Managers;

public class BottomUI : MyMonoBehaviour
{
    [SerializeField] private int goodCollectibles;
    [SerializeField] private int maxCargo;
    [SerializeField] private int badCollectibles;
    [SerializeField] private int maxStow;
    // Text
    [SerializeField] private TextMeshProUGUI GCText;
    [SerializeField] private TextMeshProUGUI MaxCargoText;
    [SerializeField] private TextMeshProUGUI BCText;
    [SerializeField] private TextMeshProUGUI MaxStowText;
    private void Awake()
    {
        AddToEventsByType();

        maxCargo = GameManager.PlayerManager.MaxCargo;
        maxStow = GameManager.PlayerManager.MaxStow;
    }
    private void Start()
    {
        MaxCargoText.text = maxCargo.ToString();
        MaxStowText.text = maxStow.ToString();
    }

    private void UpdateGoodCollectibles(object o)
    {
        goodCollectibles = (int)o;
        GCText.text = goodCollectibles.ToString();
        //var scale = BCText.transform.localScale;
        //BCText.transform.DOScale(1, 0.5f).SetEase(Ease.Flash).SmoothRewind();
    }
    private void UpdateBadCollectibles(object o)
    {
        badCollectibles = (int)o;
        BCText.text = badCollectibles.ToString();
        //var scale = BCText.transform.localScale;
        //BCText.transform.DOScale(1, 0.5f).SetEase(Ease.Flash).SmoothRewind();
    }
    
    private void RemoveToEventsByType()
    {
        GameManager.EventsManager.RemoveListener(EventsType.GoodsCollectibles, UpdateGoodCollectibles);
        GameManager.EventsManager.RemoveListener(EventsType.BannedCollectibles, UpdateBadCollectibles);
    }
    private void AddToEventsByType()
    {
        GameManager.EventsManager.AddListener(EventsType.GoodsCollectibles, UpdateGoodCollectibles);
        GameManager.EventsManager.AddListener(EventsType.BannedCollectibles, UpdateBadCollectibles);
    }
    private void OnDestroy()
    {
        RemoveToEventsByType();
    }
}
