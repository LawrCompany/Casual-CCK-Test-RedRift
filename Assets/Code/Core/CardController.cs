using System;
using Code.Core.Net;
using UniRx;


namespace Code.Core{
    public class CardController: IGetDamaged{
        #region Fields

        private readonly CardModel _model;
        public event Action<CardController> OnDeath;

        #endregion


        #region ClassLifeCycles

        public CardController(CardModel model){
            _model = model;
        }

        #endregion


        #region Methods

        public ICardModel GetModel(){
            return _model;
        }

        public void GetDamage(int value){
            _model.HealthPoints.Value -= value;
            if (_model.HealthPoints.Value <= 0){
                OnDeath?.Invoke(this);
            }
        }

        #endregion
    }
}