using System;
using UnityEngine;

namespace ER.Managers
{
    public class MyGameManager
    {
        public EventsManager EventsManager;
        public PlayerManager PlayerManager;

        public static MyGameManager Instance;
        private readonly Action _onCompleteAction;

        public MyGameManager(Action onComplete)
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError($"Two {typeof(MyGameManager)} instances exist, didn't create new one");
                return;
            }

            _onCompleteAction = onComplete;
            InitManagers();
        }

        private void InitManagers()
        {
            new EventsManager(result =>
            {
                EventsManager = (EventsManager)result;

                new PlayerManager(result =>
                {
                    PlayerManager = (PlayerManager)result;
                    _onCompleteAction.Invoke();
                });
            });
        }
    }
}

