using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


namespace Code.Core.Net{
    public class Loader{
        public async Task LoadModel(CardsModel model, ResourceSettings settings){
            model.CardsList = new List<Card>();
            var cardsCount = Random.Range(settings.MinCountCards, settings.MaxCountCards);

            for (int i = 0; i < cardsCount; i++){
                var s = await Extensions.GetRemoteTexture(settings.UrlAdressByImage);

                var card = new Card(){
                    HealthPoints = Random.Range(3, 9),
                    AttackValue = Random.Range(1, 4),
                    FaceImage = Sprite.Create(s, new Rect(0, 0, s.width, s.height), Vector2.zero),
                    Title = $"{Random.Range(0, 1000)}"
                };
                model.CardsList.Add(card);
            }

            model.OnChangeList?.Invoke();
        }
    }
}