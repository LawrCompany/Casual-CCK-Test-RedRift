using System;
using System.Collections.Generic;
using UnityEngine;


namespace Code.Core{
    [CreateAssetMenu(fileName = nameof(CardsModel), menuName = "Models/" + nameof(CardsModel))]
    public class CardsModel: ScriptableObject{
        public List<Card> CardsList{ get; set; }
        public Action OnChangeList = () => { };
    }
}