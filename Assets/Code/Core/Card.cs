﻿using UnityEngine;


namespace Code.Core{
    public class Card{
        public int HealthPoints{ get; set; }
        public string Title{ get; set; }
        public Sprite FaceImage{ get; set; }
        public int AttackValue{ get; set; }
    }
}