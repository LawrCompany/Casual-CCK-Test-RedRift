using Code.Core;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Code.Ui{
    public class UiController : MonoBehaviour{
        #region Fields

        [SerializeField]
        private CardsModel _model;
        [SerializeField]
        private ResourceSettings _settings;
        [SerializeField]
        private MainUiView _view;

        [Space]
        [Header("Simple configuration")]
        [SerializeField]
        private int _minAttackPower = -2;
        [SerializeField]
        private int _maxAttackPower = 9;

        #endregion


        #region PrivateData

        private int _selectedIndex;

        #endregion


        #region Properties

        private IGetDamaged Selected{
            get{
                if (_selectedIndex > _model.CardsList.Count - 1)
                    _selectedIndex--;
                return _model.CardsList[_selectedIndex];
            }
        }

        #endregion


        #region UnityMethods

        private void Awake(){
            _view.Init(AttackOnTheCard);
        }

        #endregion


        #region Private Methods

        private void AttackOnTheCard(){
            Selected.GetDamage(Random.Range(_minAttackPower, _maxAttackPower));
            Next();
        }

        private void Next(){
            _selectedIndex++;
            if (_selectedIndex > _model.CardsList.Count - 1){
                _selectedIndex = 0;
            }
        }

        #endregion
    }
}