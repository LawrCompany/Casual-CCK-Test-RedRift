using System;
using System.Collections;
using Code.GameBoard.DragAndDrop;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


namespace Code.GameBoard{
    public class CardView : MonoBehaviour, IDraggable{
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


        #region PrivateData

        private UnityAction _onBeginDrag;
        private UnityAction<Vector2> _onDrag;
        private UnityAction<ISlotHandler> _onEndDrag;

        #endregion


        #region methods

        public void InitInfo(Sprite faceImage, string title, in int attackValue){
            _faceImage.sprite = faceImage;
            _title.text = title;
            _attackValue.text = attackValue.ToString();
        }

        public void InitHp(in int value){
            _hp.text = value.ToString();
        }

        public void AnimationChangeHp(in int delta, float animationSpeed){
            StartCoroutine(StartCounter(delta, animationSpeed));
        }

        public void AnimationOfNotificationOfChangeHp(in int delta, float animationSpeed){
            StartCoroutine(StartNotification(delta, animationSpeed));
        }

        private IEnumerator StartCounter(int delta, float duration){
            if (delta == 0) yield return null;
            if (delta > 0)
                for (int i = 0; i < delta; i++){
                    var value = int.Parse(_hp.text);
                    value--;
                    _hp.text = value.ToString();
                    yield return new WaitForSeconds(duration / delta);
                }

            if (delta < 0)
                for (int i = 0; i > delta; i--){
                    var value = int.Parse(_hp.text);
                    value++;
                    _hp.text = value.ToString();
                    yield return new WaitForSeconds(duration / Math.Abs(delta));
                }
        }

        public void DiedAnimation(float duration){
            transform.DOScale(0, duration);
        }

        public void DestroyHimself(){
            Destroy(this);
        }

        #endregion


        #region IDraggable

        public void InitDrag(UnityAction onBeginDrag, UnityAction<Vector2> onDrag, UnityAction<ISlotHandler> onEndDrag){
            _onBeginDrag = onBeginDrag;
            _onDrag = onDrag;
            _onEndDrag = onEndDrag;
        }

        public void OnBeginDrag(){
            _onBeginDrag?.Invoke();
        }

        public void OnDrag(Vector2 position){
            _onDrag?.Invoke(position);
        }

        public void OnEndDrag(ISlotHandler slot){
            _onEndDrag?.Invoke(slot);
        }

        #endregion


        #region Private methods

        private IEnumerator StartNotification(int delta, float duration){
            var notification = Instantiate(_changedTextInHp, transform, _faceImage);

            notification.text = delta > 0 ? $"-{delta}" : $"+{Math.Abs(delta)}";
            notification.color = GetColorByValue(delta);

            delta = Math.Abs(delta);
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