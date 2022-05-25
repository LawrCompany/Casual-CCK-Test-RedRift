using Code.GameBoard.DragAndDrop;
using UnityEngine;


namespace Code.GameBoard{
    public class GameBoard : BaseView, ISlotHandler{
        public Transform Transform => transform;
        public bool IsEmpty => transform.childCount <= 0;
    }
}