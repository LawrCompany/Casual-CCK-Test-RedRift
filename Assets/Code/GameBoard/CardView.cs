using System;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;


namespace Code.GameBoard{
    public class CardView : BaseView{
        [SerializeField]
        private SpriteRenderer _faceImage;
        [SerializeField]
        private TextMeshPro _title;
        [SerializeField]
        private TextMeshPro _hp;
        [SerializeField]
        private TextMeshPro _changedTextInHp;
        
        public void Init(Sprite faceImage, string title,ReactiveProperty<int> healthPoints, int attackValue){
            _faceImage.sprite = faceImage;
            healthPoints.Subscribe(ChangeHp).AddTo(_subscriptions);
            _title.text = title;
        }

        private void ChangeHp(int value){
            SetDefaultNotification();
            var oldValue = int.Parse(_hp.text);
            var delta = oldValue - value;
            
            _hp.text = value.ToString();
            
            if(oldValue != 0)
                StartNotificationFromChangeHp(delta);
        }

        private void StartNotificationFromChangeHp(int delta){
            _changedTextInHp.text = delta.ToString();
            _changedTextInHp.transform.DOMove(_changedTextInHp.transform.position + Vector3.up, 0.5f);
            _changedTextInHp.DOFade(0, 0.5f);
        }

        private void SetDefaultNotification(){
            _changedTextInHp.transform.position = _hp.transform.position;
            _changedTextInHp.text = "";
        }
    }
}