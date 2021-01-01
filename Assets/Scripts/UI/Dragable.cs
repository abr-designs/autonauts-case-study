using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dragable : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform _currentTransform;

    private static DragController _dragController; 

    private void Start()
    {
        _currentTransform = gameObject.transform as RectTransform;

        if(!_dragController)
            _dragController = UIManager.Instance.DragController;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button != PointerEventData.InputButton.Left)
            return;
        
        _dragController.OnDragStarted(_currentTransform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(eventData.button != PointerEventData.InputButton.Left)
            return;
        
        _dragController.OnDrag(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.button != PointerEventData.InputButton.Left)
            return;
        
        _dragController.OnDragCompleted();
    }
}
