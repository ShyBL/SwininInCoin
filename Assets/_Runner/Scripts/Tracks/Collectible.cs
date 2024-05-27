using System;
using DG.Tweening;
using UnityEngine;
using ER.Managers;

public class Collectible : MyMonoBehaviour
{
    [SerializeField] public CollectibleType type;
    [SerializeField] private Ease MoveEase; 
    [SerializeField] private Ease ScaleEase; 
    private Player _player;
    private PlayerManager _playerManager;
    
    void Start()
    {
        _player = FindFirstObjectByType<Player>();
        _playerManager = GameManager.PlayerManager;
    }
    private void AddCollectible()
    {
        switch (type)
        {
            case CollectibleType.GoodsCollectibles:
                if (!_playerManager.IsFull(type))
                {
                    _playerManager.ChangeCollectibles(type,EventsType.GoodsCollectibles,1);
                }
                
                break;
            case CollectibleType.BannedCollectibles:
                if (!_playerManager.IsFull(type))
                {
                    _playerManager.ChangeCollectibles(type,EventsType.BannedCollectibles, 1);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            transform.DOScale(0, 0.3f);
            transform.DOJump(_player.CollectPoint.position, 1f, 1, 0.5f).OnComplete(DoCollectible);
        }
    }

    private void DoCollectible()
    {
        if (!GameManager.PlayerManager.IsFull(type))
        {
            AddCollectible();
            _player.DoParticle(type);
            transform.DOScale(0, 0.3f);
        }
    }


}

public enum CollectibleType
{
    GoodsCollectibles,
    BannedCollectibles
}
