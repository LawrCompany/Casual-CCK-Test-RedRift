using UnityEngine;
using UnityEngine.Events;


namespace Code.GameBoard.DragAndDrop{
    public interface IDraggable{
        void InitDrag(UnityAction onBeginDrag, UnityAction<Vector2> onDrag, UnityAction<ISlotHandler> onEndDrag);
        void OnBeginDrag();
        void OnDrag(Vector2 position);
        void OnEndDrag(ISlotHandler slot);
    }
}