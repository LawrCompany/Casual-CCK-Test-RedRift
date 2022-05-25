using System;
using Code.GameBoard;
using Code.GameBoard.Views;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Code.Ui{
    internal class MainUiView: BaseView{
        #region Fields

        [SerializeField]
        private Button _attackButton;

        #endregion

        public void Init(Action attackToCard){
            _attackButton.OnClickAsObservable().Subscribe(_ =>attackToCard.Invoke()).AddTo(_subscriptions);
        }
    }
}