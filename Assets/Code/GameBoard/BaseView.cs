using UniRx;
using UnityEngine;


namespace Code.GameBoard{
    public class BaseView: MonoBehaviour{
        protected CompositeDisposable _subscriptions = new CompositeDisposable();
        public virtual void OnDestroy()
        {
            _subscriptions?.Dispose();
        }
    }
}