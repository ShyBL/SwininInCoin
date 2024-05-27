using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using ER.Managers;
using UnityEngine.UI;

public class Popup : MyMonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI Description;
    [SerializeField] public Button button;
    [SerializeField] private GameObject PayoffGoods;
    [SerializeField] private GameObject PayoffContraband;
    [SerializeField] private GameObject PayoffZone;
    [SerializeField] private GameObject UpgradesZone;
    [SerializeField] private RectTransform Money;
    [SerializeField] private RectTransform MoneyUI;

    private TrackType _trackType;
    private Player playerMove;
    [SerializeField] private Dialogue playerDialogue;

    private int GoodCollectiblesAmount => GameManager.PlayerManager.GoodCollectibles;
    private int BadCollectiblesAmount => GameManager.PlayerManager.BadCollectibles;
    
    public void InitializePopup(string name, string description, TrackType type, Player player)
    {
        _trackType = type; 
        Title.text = name;
        //Description.text = description;
        playerMove = player;

        if (GoodCollectiblesAmount == 0 && BadCollectiblesAmount == 0)
        {
            _trackType = type; 
            Title.text = name;
            Description.text = "You have nothing for me...";
            playerMove = player;
            return;
        }
        
        playerDialogue.Initialize(description,2,playerMove);
        
        //int amount = 0;
        switch (_trackType)
        {
            case TrackType.None:
                break;
            case TrackType.End:
                SpawnEnergy(SpendLeftovers());
                StartCoroutine(DelayedDoUpgradeScreen(1f));
                break;
            case TrackType.Fisherman:
                SpawnMoney(FishermanPayoff());
                StartCoroutine(DelayedDoUpgradeScreen(1f));
                break;
            case TrackType.Pirate:
                SpawnMoney(PiratePayoff());
                StartCoroutine(DelayedDoUpgradeScreen(1f));
                break;
            case TrackType.Harbor:
                SpawnMoney(HarborPayoff());
                StartCoroutine(DelayedDoUpgradeScreen(1f));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }
    
    private IEnumerator DelayedDoUpgradeScreen(float delay)
    {
        yield return new WaitForSeconds(delay);
        DoUpgradeScreen();
    }

    private void DoUpgradeScreen()
    {
        UpgradesZone.SetActive(true);
    }
    
    private int SpendLeftovers()
    {
        int Energypayoff = GameManager.PlayerManager.BadCollectibles + GameManager.PlayerManager.GoodCollectibles;
        var payOff = Energypayoff;
        GameManager.PlayerManager.GetEnergyForLeftovers(payOff);
        GameManager.PlayerManager.ChangeCollectibles(CollectibleType.GoodsCollectibles,EventsType.GoodsCollectibles,0, true);
        GameManager.PlayerManager.ChangeCollectibles(CollectibleType.BannedCollectibles,EventsType.BannedCollectibles,0, true);
        return payOff;
    }

    private void SpawnEnergy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var randomDuration = Random.Range(0.5f, 1f);
            var coin = Instantiate(Money,this.transform);
            coin.transform.DOMove(MoneyUI.transform.position, randomDuration).SetEase(Ease.OutQuad);
        }

    }
    
    private void SpawnMoney(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var randomDuration = Random.Range(0.5f, 1f);
            var coin = Instantiate(Money,this.transform);
            coin.transform.DOMove(MoneyUI.transform.position, randomDuration).SetEase(Ease.OutQuad);
        }

    }
    
    private int FishermanPayoff()
    {
        int Goodpayoff = 0;
        var GoodAmount = GoodCollectiblesAmount;
        var BadAmount = BadCollectiblesAmount;
        
        for (int i = 1; i <= GoodAmount; i++)
        {
            var temp = Instantiate(PayoffGoods);
            temp.transform.GetChild(0).gameObject.SetActive(true);
            temp.transform.SetParent(PayoffZone.transform);
            
            Goodpayoff++;
        }

        for (int i = 1; i <= BadAmount; i++)
        {
            var temp = Instantiate(PayoffContraband);
            temp.transform.GetChild(1).gameObject.SetActive(true);
            temp.transform.SetParent(PayoffZone.transform);
        }

        var payOff = Goodpayoff;
        GameManager.PlayerManager.PayoffCollectibles(payOff);
        GameManager.PlayerManager.ChangeCollectibles(CollectibleType.GoodsCollectibles,EventsType.GoodsCollectibles,0, true);
        return payOff;
    }
    
    private int PiratePayoff()
    {
        int Goodpayoff = 0;
        int Badpayoff = 0;
        
        var GoodAmount = GoodCollectiblesAmount;
        var BadAmount = BadCollectiblesAmount;
        
        for (int i = 1; i <= GoodAmount; i++)
        {
            var temp = Instantiate(PayoffGoods);
            temp.transform.GetChild(1).gameObject.SetActive(true);
            temp.transform.SetParent(PayoffZone.transform);

            Goodpayoff++;
        }

        for (int i = 1; i <= BadAmount; i++)
        {
            var temp = Instantiate(PayoffContraband);
            temp.transform.GetChild(0).gameObject.SetActive(true);
            temp.transform.SetParent(PayoffZone.transform);
            
            Badpayoff++;
        }

        if (Badpayoff < Goodpayoff)
        {
            var halfGood = Goodpayoff / 2;
            GameManager.PlayerManager.PayoffCollectibles(Badpayoff + halfGood);
            GameManager.PlayerManager.ChangeCollectibles(CollectibleType.GoodsCollectibles,EventsType.GoodsCollectibles,0, true);
            GameManager.PlayerManager.ChangeCollectibles(CollectibleType.BannedCollectibles,EventsType.BannedCollectibles,0, true);
            
            return Badpayoff + halfGood;
        }
        else
        {
            var payOff = Goodpayoff + Badpayoff;
            GameManager.PlayerManager.PayoffCollectibles(payOff);
            GameManager.PlayerManager.ChangeCollectibles(CollectibleType.GoodsCollectibles,EventsType.GoodsCollectibles,0, true);
            GameManager.PlayerManager.ChangeCollectibles(CollectibleType.BannedCollectibles,EventsType.BannedCollectibles,0, true);

            return payOff;
        }
    }
    
    private int HarborPayoff()
    {
        int Goodpayoff = 0;
        int Badpayoff = 0;

        for (int i = 1; i <= GameManager.PlayerManager.GoodCollectibles; i++)
        {
            var temp = Instantiate(PayoffGoods);
            temp.transform.GetChild(0).gameObject.SetActive(true);
            temp.transform.SetParent(PayoffZone.transform);
            
            Goodpayoff++;
        }

        for (int i = 1; i <= GameManager.PlayerManager.BadCollectibles; i++)
        {
            var temp = Instantiate(PayoffContraband);
            temp.transform.GetChild(1).gameObject.SetActive(true);
            temp.transform.SetParent(PayoffZone.transform);

            Badpayoff++;
        }

        var payOff = Goodpayoff - Badpayoff;
        GameManager.PlayerManager.PayoffCollectibles(payOff);
        GameManager.PlayerManager.ChangeCollectibles(CollectibleType.GoodsCollectibles,EventsType.GoodsCollectibles,0, true);
        GameManager.PlayerManager.ChangeCollectibles(CollectibleType.BannedCollectibles,EventsType.BannedCollectibles,0, true);

        return payOff;
    }
    
    
    public void Release()
    {
        playerMove.ReleaseFromStop();
        foreach (Transform child in PayoffZone.transform)
        {
            Destroy(child.gameObject);
        }
        
        UpgradesZone.SetActive(false);
        gameObject.SetActive(false);
        
    }
}