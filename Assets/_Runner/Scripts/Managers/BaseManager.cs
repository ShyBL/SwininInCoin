using System;
using System.Threading.Tasks;

namespace ER.Managers
{
    public class BaseManager
    {
        protected MyGameManager MyGameManager => MyGameManager.Instance;
        private Action<BaseManager> onCompleteAction;

        protected BaseManager(Action<BaseManager> onComplete)
        {
            onCompleteAction = onComplete;
        }

        protected async void OnInitComplete()
        {
            await Task.Delay(500);
            onCompleteAction.Invoke(this);
        }
    }
}