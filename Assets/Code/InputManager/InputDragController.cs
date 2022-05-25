using Code.GameBoard.DragAndDrop;
using UnityEngine;


namespace Code.InputManager{
    public class InputDragController : MonoBehaviour{
        #region PrivateData

        bool _canMove;
        bool _dragging;
        private IDraggable _draggable;

        #endregion


        private void Update(){
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0) || Input.touchCount == 1){
                if (_draggable == null){
                    var target = Physics2D.OverlapPoint(mousePos);
                    if (!target) return;
                    if (target.TryGetComponent(out IDraggable draggable)){
                        Debug.Log($"{draggable}");
                        if (_draggable != draggable)
                            draggable.OnBeginDrag();
                        _draggable = draggable;
                        _canMove = true;
                    } else{
                        _canMove = false;
                    }

                    if (_canMove){
                        _dragging = true;
                    }
                }
            }

            if (_dragging){
                _draggable?.OnDrag(mousePos);
            }

            if (Input.GetMouseButtonUp(0) || Input.touchCount <= 0){
                _canMove = false;
                _dragging = false;
                if (_draggable != null){
                    var slots = new Collider2D[2];
                    Physics2D.OverlapPointNonAlloc(mousePos, slots);
                    foreach (var slot in slots){
                        if (!slot){
                            _draggable.OnEndDrag(null);
                            _draggable = null;
                            return;
                        }
                        if (slot.TryGetComponent(out ISlotHandler board)){
                            if(board.IsEmpty){
                                _draggable.OnEndDrag(board);
                                _draggable = null;
                                return;
                            }
                        }
                    }
                    _draggable?.OnEndDrag(null);
                    _draggable = null;
                }
            }
        }
    }
}