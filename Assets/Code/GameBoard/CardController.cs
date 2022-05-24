using System.Linq;
using Code.Core;
using UnityEngine;


namespace Code.GameBoard{
    public class CardController : MonoBehaviour{
        [SerializeField]
        private ResourceSettings _settings;
        [SerializeField]
        private CardsModel _model;
        [SerializeField]
        private SpriteRenderer _packOfCards;

        private void Awake(){
            _model.OnChangeList += OnChangeList;
        }

        private void OnChangeList(){
            Debug.Log($"{this} - List Changed");
            var list = _packOfCards.transform.GetComponentsInChildren<CardView>().ToList();
            if (list.Count == 0){
                foreach (var card in _model.CardsList){
                    var cardView = Instantiate(_settings.CardTemplate, _packOfCards.transform, false);
                    cardView.Init(card.FaceImage);
                    list.Add(cardView);
                }
            }
        }
    }
}