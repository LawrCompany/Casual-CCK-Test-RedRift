using UniRx;
using UnityEngine;


namespace Code.Core{
    public class CardController{
        public ReactiveProperty<int> HealthPoints{ get; set; }
        public string Title{ get; set; }
        public Sprite FaceImage{ get; set; }
        public int AttackValue{ get; set; }
    }
}