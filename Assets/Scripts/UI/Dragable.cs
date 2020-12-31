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
        _dragController.OnDragStarted(_currentTransform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _dragController.OnDrag(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _dragController.OnDragCompleted();
    }
}
