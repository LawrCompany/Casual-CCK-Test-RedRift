﻿using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;


namespace Code.GameBoard{
    public class CardView : MonoBehaviour{
        #region Fields

        [SerializeField]
        public SpriteRenderer _faceImage;
        [SerializeField]
        public TextMeshPro _title;
        [SerializeField]
        private TextMeshPro _hp;
        [SerializeField]
        public TextMeshPro _changedTextInHp;
        [SerializeField]
        public TextMeshPro _attackValue;

        #endregion


        #region methods

        public void Init(Sprite faceImage, string title, in int attackValue){
            _faceImage.sprite = faceImage;
            _title.text = title;
            _attackValue.text = attackValue.ToString();
        }

        public void ChangeHp(in int value){
            _hp.text = value.ToString();
        }

        public void StartNotificationFromChangeHp(in int delta, float settingsAnimationSpeed){
            StartCoroutine(StartNotification(delta, settingsAnimationSpeed));
        }

        public void DiedAnimation(float duration){
            transform.DOScale(0, duration);
        }

        public void DestroyHimself(){
            Destroy(this);
        }

        #endregion


        #region Private methods

        private IEnumerator StartNotification(int delta, float duration){
            var notification = Instantiate(_changedTextInHp);
            notification.transform.position = _hp.transform.position;

            notification.text = delta > 0 ? $"-{delta}" : $"{delta}";
            notification.color = GetColorByValue(delta);

            notification.transform.DOMove(_changedTextInHp.transform.position + Vector3.up, duration);
            notification.DOFade(0, duration);
            yield return new WaitForSeconds(delta);
            Destroy(notification);
        }

        private Color GetColorByValue(in int delta){
            if (delta > 0)
                return Color.red;
            if (delta < 0)
                return Color.green;
            return Color.white;
        }

        #endregion
    }
}