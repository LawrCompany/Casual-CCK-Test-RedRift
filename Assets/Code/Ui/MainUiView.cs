using System;
using Code.GameBoard;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Code.Ui{
    internal class MainUiView: BaseView{
        [SerializeField]
        private Button _attackButton;

        public void Init(Action attackToCard){
            _attackButton.OnClickAsObservable().Subscribe(_ =>attackToCard.Invoke()).AddTo(_subscriptions);
        }
    }
}