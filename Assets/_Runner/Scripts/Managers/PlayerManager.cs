using System;
using UnityEngine;

namespace ER.Managers
{
    public class PlayerManager : BaseManager
    {
        public float InitialForwardSpeed;
        public float InitialSidewaysSpeed;
    
        public int GoodCollectibles;
        public int MaxCargo;
        public int BadCollectibles;
        public int MaxStow;

        public int Money;
        public int Energy;

        public ShipType CurrentShip;
    
        public PlayerManager(Action<BaseManager> onComplete) : base(onComplete)
        {
            InitialForwardSpeed = 10;
            InitialSidewaysSpeed = 20;
        
            GoodCollectibles = 0;
            BadCollectibles = 0;
            MaxCargo = 10;
            MaxStow = 3; 
            Money = 0;
            Energy = 15;

            CurrentShip = ShipType.Cadet;
            OnInitComplete();
        }
    

        public void GetEnergyForLeftovers(int amount)
        {
            int newAmount = Energy + amount;
            if (newAmount < 0)
            {
                Debug.Log("Cannot decrease CurrencyAmount below 0.");
                return;
            }
            Energy += amount;
        }
    
        public void PayoffCollectibles(int amount)
        {
            int newAmount = Money + amount;
            if (newAmount < 0)
            {
                Debug.Log("Cannot decrease CurrencyAmount below 0.");
                return;
            }
            Money += amount;
        }
    
        public void ChangeCollectibles(CollectibleType collectibleType, EventsType eventsType, int amount, bool remove = false)
        {
            if (!remove)
            {
                switch (collectibleType)
                {
                    case CollectibleType.GoodsCollectibles:
                        if (IsFull(collectibleType)) return;

                        GoodCollectibles += amount;
                        MyGameManager.EventsManager.InvokeEvent(eventsType, GoodCollectibles);
                
                        break;
            
                    case CollectibleType.BannedCollectibles:
                        if (IsFull(collectibleType)) return;

                        BadCollectibles += amount;
                        MyGameManager.EventsManager.InvokeEvent(eventsType, BadCollectibles);

                        break;
                }
            }
            else
            {
                switch (collectibleType)
                {
                    case CollectibleType.GoodsCollectibles:

                        GoodCollectibles = amount;
                        MyGameManager.EventsManager.InvokeEvent(eventsType, GoodCollectibles);
                
                        break;
            
                    case CollectibleType.BannedCollectibles:

                        BadCollectibles = amount;
                        MyGameManager.EventsManager.InvokeEvent(eventsType, BadCollectibles);

                        break;
                } 
            }
        }
    
        public bool IsFull(CollectibleType type)
        {
            switch (type)
            {
                case CollectibleType.GoodsCollectibles:
                    return GoodCollectibles == MaxCargo;
            
                case CollectibleType.BannedCollectibles:
                    return BadCollectibles == MaxStow;
            }

            return false;
        }
    }
    
}