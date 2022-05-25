using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code.Core;
using Code.Core.Net;
using DG.Tweening;
using UniRx;
using UnityEngine;


namespace Code.GameBoard{
    public class PackOfCardsController : MonoBehaviour{
        #region fields

        [SerializeField]
        private ResourceSettings _settings;
        [SerializeField]
        private CardsModel _model;
        [SerializeField]
        private PackOfCardsView _view;

        #endregion


        #region PrivateData

        private CompositeDisposable _subscriptions = new CompositeDisposable();
        private List<CardViewController> _packOfCards = new List<CardViewController>();

        #endregion


        #region Unity Methods

        private async void Awake(){
            DOTween.Init();
            _model.OnChangeList += OnChangeList;
            await Factory.CreateControllersOfCards(_model, _settings);
        }

        #endregion


        #region ClassLifeCycles

        ~PackOfCardsController(){
            _model.OnChangeList -= OnChangeList;
            _subscriptions?.Dispose();
        }

        #endregion


        #region Private methods

        private async void OnChangeList(){
            Debug.Log($"{this} - List Changed");
            var list = _view.transform.GetComponentsInChildren<CardView>().ToList();
            if (list.Count == 0){
                foreach (var card in _model.CardsList){
                    card.OnDeath += OneCardDied;
                    var cardViewController = new CardViewController(_settings, card, _view.transform);
                    _packOfCards.Add(cardViewController);
                }
            }

            await SetDefaultPositionOnCards();
        }

        private async void OneCardDied(IGetDamaged item){
            item.OnDeath -= OneCardDied;
            _model.CardsList.Remove(item as CardController);

            foreach (var controller in _packOfCards){
                if (controller.IsReferenceEquals(item as CardController)){
                    _packOfCards.Remove(controller);
                    break;
                }
            }

            await SetDefaultPositionOnCards();
        }

        private async Task SetDefaultPositionOnCards(){
            //set normal positions
            var leftBoarder = _view._leftBoarder.position;
            var rightBoarder = _view._rightBoarder.position;
            var wight = leftBoarder - rightBoarder;
            var verticalMagnitude = (_view._anchorCenter.position - leftBoarder).magnitude;
            for (var i = 0; i < _packOfCards.Count; i++){
                var card = _packOfCards[i];
                card.MoveTo(new Vector3(
                    leftBoarder.x - wight.x / _packOfCards.Count * i,
                    verticalMagnitude + _view._anchorCenter.position.y,
                    -i));
            }

            await Task.Delay((int) (_settings.AnimationSpeed * 1000));
            //rotate to anchor
            foreach (var card in _packOfCards){
                card.RotateTo(_view._anchorCenter.position.x);
            }
        }

        #endregion
    }
}