using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code.Core;
using Code.Core.Models;
using Code.Core.Net;
using Code.Data;
using Code.GameBoard.Views;
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
        private CardViewController _cashViewController;

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
            foreach (var cardController in _model.CardsList){
                cardController.OnAddedToPack -= OnAddedToPack;
                cardController.OnRemoveFromPack -= OnRemoveFromPack;
            }

            _model.OnChangeList -= OnChangeList;
            _subscriptions?.Dispose();
        }

        #endregion


        #region Private methods

        private async void OnChangeList(){
            var list = _view.transform.GetComponentsInChildren<CardView>().ToList();
            if (list.Count == 0){
                foreach (var card in _model.CardsList){
                    card.OnRemoveFromPack += OnRemoveFromPack;
                    card.OnAddedToPack += OnAddedToPack;
                    card.OnReturnToLastPosition += OnReturnToLastPosition;
                    card.OnDeath += OneCardDied;
                    var cardViewController = new CardViewController(_settings, card, _view.transform);
                    _packOfCards.Add(cardViewController);
                }
            }

            await SetDefaultPositionOnCards();
        }

        private async void OnReturnToLastPosition(CardController controller){
            await SetDefaultPositionOnCards();
        }

        private void OnAddedToPack(CardController addedController){
            if (_cashViewController != null){
                _packOfCards.Add(_cashViewController);
                _model.OnChangeList?.Invoke();
            }
        }

        private void OnRemoveFromPack(CardController removeController){
            foreach (var viewController in _packOfCards){
                if (viewController.IsReferenceEquals(removeController)){
                    _cashViewController = viewController;
                    _packOfCards.Remove(viewController);
                    _model.OnChangeList?.Invoke();
                    return;
                }
            }
        }

        private async void OneCardDied(IGetDamaged item){
            item.OnDeath -= OneCardDied;
            _model.CardsList.Remove(item as CardController);
            _model.OnChangeList?.Invoke();

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

            _view._anchorCenter.LookAt(leftBoarder);
            var vectorFrom = _view._anchorCenter.forward;
            _view._anchorCenter.LookAt(rightBoarder);
            var vectorTo = _view._anchorCenter.forward;
            
            for (var i = 0; i < _packOfCards.Count; i++){
                var card = _packOfCards[i];
                var angle = - Vector3.Angle(vectorFrom, vectorTo) / _packOfCards.Count * i;
                angle = angle * Mathf.Deg2Rad - (210 * Mathf.Deg2Rad);

                card.MoveTo(parent: _view.transform, new Vector3(
                    Mathf.Cos(angle) + _view._anchorCenter.position.x,
                    Mathf.Sin(angle) + _view._leftBoarder.position.y,
                    -i));
            }

            await Task.Delay((int) (_settings.AnimationSpeed * 1000));
            //rotate to anchor
            foreach (var card in _packOfCards){
                card.RotateTo(_view._anchorCenter.position);
            }
        }

        #endregion
    }
}