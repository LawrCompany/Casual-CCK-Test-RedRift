using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;


namespace Code.GameBoard{
    public class CardView : MonoBehaviour{
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

        public void Init(Sprite faceImage, string title, in int attackValue){
            _faceImage.sprite = faceImage;
            _title.text = title;
            _attackValue.text = attackValue.ToString();
        }

        public void ChangeHp(in int value){
            _hp.text = value.ToString();
        }

        public void StartNotificationFromChangeHp(in int delta, float settingsAnimationSpeed){
            StartCoroutine(StartNotification(delta,settingsAnimationSpeed));
        }
        
        private IEnumerator StartNotification(int delta, float duration){
            var notification = Instantiate(_changedTextInHp, this.transform, true);
            notification.text = delta.ToString();
            notification.transform.DOMove(_changedTextInHp.transform.position + Vector3.up, duration);
            notification.DOFade(0, duration);
            yield return Task.Delay((int)duration *1000);
            Destroy(notification);
        }
    }
}