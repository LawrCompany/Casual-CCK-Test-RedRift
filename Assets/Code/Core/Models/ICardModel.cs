﻿using UniRx;
using UnityEngine;


namespace Code.Core.Models{
    public interface ICardModel{
        ReactiveProperty<int> HealthPoints{ get; set; }
        string Title{ get; set; }
        Sprite FaceImage{ get; set; }
        int AttackValue{ get; set; }
    }
}