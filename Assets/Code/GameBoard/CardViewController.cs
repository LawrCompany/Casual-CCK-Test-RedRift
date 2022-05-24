using System.Threading.Tasks;
using Code.Core;
using Code.Core.Net;
using DG.Tweening;
using UniRx;
using UnityEngine;


namespace Code.GameBoard{
    internal class CardViewController{
        private readonly ResourceSettings _settings;
        private readonly CardController _cardController;
        private readonly CardView _view;
        private readonly ICardModel _model;
        
        protected CompositeDisposable _subscriptions = new CompositeDisposable();

        private int _oldHpValue = 0;

        public CardViewController(ResourceSettings settings, CardController cardController, CardView view){
            _settings = settings;
            _cardController = cardController;
            _view = view;

            _model = _cardController.GetModel();
            _model.HealthPoints.Subscribe(ChangeHp).AddTo(_subscriptions);
            _cardController.OnDeath += OnDied;
            _view.Init(_model.FaceImage,_model.Title,_model.AttackValue);
        }

        private async void OnDied(CardController obj){
            _view.DiedAnimation(_settings.AnimationSpeed);
            await Task.Delay((int)(1000*_settings.AnimationSpeed));
            _view.DestroyHimself();
        }

        public void MoveTo(in Vector3 endPosition){
            _view.transform.DOJump(endPosition, jumpPower: 1, numJumps: 1, _settings.AnimationSpeed);
        }

        public void RotateTo(in float positionAnchor){
            var rotationCoefficient =
                2; //I know that it`s bad practice, but I couldn't turn the cards in the right direction
            _view.transform.DORotate(new Vector3(0, 0,
                    (-_view.transform.position.x + positionAnchor) * rotationCoefficient),
                _settings.AnimationSpeed);
        }

        private void ChangeHp(int value){
            _view.ChangeHp(value);

            if (_oldHpValue != 0){
                var delta = _oldHpValue - value;
                // Debug.Log($"old value:{_oldHpValue}|delta:{delta}");
                _view.StartNotificationFromChangeHp(delta, _settings.AnimationSpeed);
            }

            _oldHpValue = value;
        }

        ~CardViewController(){
            _subscriptions?.Dispose();
        }

        public bool IsReferenceEquals(CardController item){
            return object.ReferenceEquals(item, _cardController);
        }
    }
}