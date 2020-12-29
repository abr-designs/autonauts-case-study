using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform _currentTransform;
    private Image _image;
    
    private GameObject _mainContent;
    private Vector3 _currentPosition;

    private int _totalChild;

    private static Canvas _canvas;

    [SerializeField]
    private VerticalLayoutGroup hoveredGameObject;
    
    private void Start()
    {
        _currentTransform = gameObject.transform as RectTransform;
        _image = GetComponent < Image>();

        if (!_canvas)
        {
            _canvas = GetComponentInParent<Canvas>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        /*_currentPosition = _currentTransform.position;
        _mainContent = _currentTransform.parent.gameObject;
        _totalChild = _mainContent.transform.childCount;*/
        
        _currentTransform.SetParent(_canvas.transform, true);
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == null)
            return;
        
        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<VerticalLayoutGroup>() is VerticalLayoutGroup vlg)
        {
            hoveredGameObject = vlg;
        }

        /*var pos = _currentTransform.position;
        pos.y = eventData.position.y;
        
        /*_currentTransform.position =
            new Vector3(_currentTransform.position.x, eventData.position.y, _currentTransform.position.z);#1#

        for (var i = 0; i < _totalChild; i++)
        {
            if (i == _currentTransform.GetSiblingIndex()) 
                continue;
            
            var otherTransform = _mainContent.transform.GetChild(i);
            var otherTransformPosition = otherTransform.position;
            var distance = (int) Vector3.Distance(pos, otherTransformPosition);
            
            if (distance > 10) 
                continue;
            
            /*var otherTransformOldPosition = otherTransform.position;
            otherTransformPosition.y = _currentPosition.y;
                    
            pos.y = otherTransformOldPosition.y;#1#
                    
            _currentTransform.SetSiblingIndex(otherTransform.GetSiblingIndex());
            //_currentPosition = pos;

            //otherTransform.position = otherTransformPosition;
        }

        //_currentTransform.position = pos;*/
        _currentTransform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //_currentTransform.position = _currentPosition;
        
    }
}
