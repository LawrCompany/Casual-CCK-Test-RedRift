using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code.Core;
using DG.Tweening;
using UnityEngine;


namespace Code.GameBoard{
    public class PackOfCardsController : MonoBehaviour{
        [SerializeField]
        private ResourceSettings _settings;
        [SerializeField]
        private CardsModel _model;
        [SerializeField]
        private PackOfCardsView _view;

        private List<CardView> _packOfCards;

        private void Awake(){
            DOTween.Init();

            _model.OnChangeList += OnChangeList;
        }

        private async void OnChangeList(){
            Debug.Log($"{this} - List Changed");
            _packOfCards = _view.transform.GetComponentsInChildren<CardView>().ToList();
            if (_packOfCards.Count == 0){
                foreach (var card in _model.CardsList){
                    var cardView = Instantiate(_settings.CardTemplate, _view.transform, false);
                    cardView.Init(card.FaceImage);
                    _packOfCards.Add(cardView);
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
                card.transform.DOJump(new Vector3(
                        leftBoarder.x - wight.x / _packOfCards.Count * i,
                        verticalMagnitude + _view._anchorCenter.position.y,
                        -i),
                    1f, 1, _settings.AnimationSpeed);
            }

            await Task.Delay((int) (_settings.AnimationSpeed * 1000));
            foreach (var card in _packOfCards){
                card.transform.DORotate(new Vector3(0, 0,
                    (-card.transform.position.x + _view._anchorCenter.position.x) * 2), 0.5f);
            }
        }

        ~PackOfCardsController(){
            _model.OnChangeList -= OnChangeList;
        }
    }
}