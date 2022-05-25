using UnityEngine;


namespace Code.GameBoard.DragAndDrop{
    public interface ISlotHandler{
        Transform Transform{ get; }
        bool IsEmpty{ get; }
    }
}