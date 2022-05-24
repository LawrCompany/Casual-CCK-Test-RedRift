using System;
using Code.Core.Net;
using UniRx;


namespace Code.Core{
    public class CardController: IGetDamaged{
        private readonly CardModel _model;
        
        public event Action<CardController> OnDeath;

        public CardController(CardModel model){
            _model = model;
        }

        public ICardModel GetModel(){
            return _model;
        }

        public void GetDamage(int value){
            _model.HealthPoints.Value -= value;
            if (_model.HealthPoints.Value <= 0){
                OnDeath?.Invoke(this);
            }
        }
    }
}