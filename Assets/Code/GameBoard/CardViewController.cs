using System.Threading.Tasks;
using Code.Core;
using Code.Core.Net;
using DG.Tweening;
using UniRx;
using UnityEngine;


namespace Code.GameBoard{
    public class CardViewController{
        #region Fields

        private readonly ResourceSettings _settings;
        private readonly CardController _cardController;
        private readonly CardView _view;
        private readonly ICardModel _model;

        #endregion


        #region PrivateData

        private int _oldHpValue = 0;

        #endregion


        #region Properties

        protected CompositeDisposable _subscriptions = new CompositeDisposable();

        #endregion


        #region ClassLifeCycles

        public CardViewController(ResourceSettings settings, CardController cardController, Transform placeForView){
            _settings = settings;
            _cardController = cardController;

            _view = Object.Instantiate(_settings.CardTemplate, placeForView, false);
            ;

            _model = _cardController.GetModel();
            _model.HealthPoints.Subscribe(ChangeHp).AddTo(_subscriptions);
            _cardController.OnDeath += OnDied;
            _view.Init(_model.FaceImage, _model.Title, _model.AttackValue);
        }

        ~CardViewController(){
            _cardController.OnDeath -= OnDied;
            _subscriptions?.Dispose();
        }

        #endregion


        #region Methods

        public bool IsReferenceEquals(CardController item){
            return ReferenceEquals(item, _cardController);
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

        #endregion


        #region Private methods

        private void ChangeHp(int value){
            _view.ChangeHp(value);

            if (_oldHpValue != 0){
                var delta = _oldHpValue - value;
                _view.StartNotificationFromChangeHp(delta, _settings.AnimationSpeed);
            }

            _oldHpValue = value;
        }

        private async void OnDied(CardController obj){
            _view.DiedAnimation(_settings.AnimationSpeed);
            await Task.Delay((int) (1000 * _settings.AnimationSpeed));
            _view.DestroyHimself();
        }

        #endregion
    }
}