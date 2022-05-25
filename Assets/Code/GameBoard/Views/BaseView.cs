using UniRx;
using UnityEngine;


namespace Code.GameBoard.Views{
    public class BaseView: MonoBehaviour{
        #region Properties

        protected CompositeDisposable _subscriptions = new CompositeDisposable();

        #endregion


        #region Unity methods

        public virtual void OnDestroy()
        {
            _subscriptions?.Dispose();
        }

        #endregion
    }
}