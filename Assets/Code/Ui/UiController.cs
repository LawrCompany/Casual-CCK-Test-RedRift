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
        [SerializeField]
        private Transform _gameOverView;

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
            _model.OnChangeList += OnChangeList;
            _gameOverView.gameObject.SetActive(false);
        }

        #endregion


        ~UiController(){
            _model.OnChangeList -= OnChangeList;
        }


        #region Private Methods

        private void OnChangeList(){
            if (_model.CardsList.Count == 0){
                FinishTheGame();
            }
        }

        private void AttackOnTheCard(){
            Selected.GetDamage(Random.Range(_settings.MinAttackPower,_settings.MaxAttackPower));
            Next();
        }

        private void Next(){
            _selectedIndex++;
            if (_selectedIndex > _model.CardsList.Count - 1){
                _selectedIndex = 0;
            }
        }

        private void FinishTheGame(){
            _gameOverView.gameObject.SetActive(true);
        }

        #endregion
    }
}