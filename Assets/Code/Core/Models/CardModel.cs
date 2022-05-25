using UniRx;
using UnityEngine;


namespace Code.Core.Models{
    public class CardModel : ICardModel{
        public ReactiveProperty<int> HealthPoints{ get; set; }
        public string Title{ get; set; }
        public Sprite FaceImage{ get; set; }
        public int AttackValue{ get; set; }
    }
}