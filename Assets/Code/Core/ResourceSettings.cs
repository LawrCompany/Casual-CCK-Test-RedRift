using Code.GameBoard;
using UnityEngine;


namespace Code.Core{
    [CreateAssetMenu(fileName = nameof(ResourceSettings), menuName = "Settings/" + nameof(ResourceSettings))]
    public class ResourceSettings : ScriptableObject{
        public CardView CardTemplate;
        public int MinCountCards = 4;
        public int MaxCountCards = 6;
        public string UrlAdressByImage = "https://picsum.photos/200/300";
        public float AnimationSpeed = 0.5f;
        
        [Space]
        [Header("Simple configuration attacker")]
        [SerializeField]
        public int MinAttackPower = -2;
        [SerializeField]
        public int MaxAttackPower = 9;
    }
}