using TMPro;
using UnityEngine;


namespace Code.GameBoard{
    public class CardView : MonoBehaviour{
        [SerializeField]
        private SpriteRenderer _faceImage;
        [SerializeField]
        private TextMeshPro _title;
        [SerializeField]
        private TextMeshPro _hp;

        public void Init(Sprite cardFaceImage){
            _faceImage.sprite = cardFaceImage;
        }
    }
}