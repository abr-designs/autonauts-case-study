using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DragController))]
public class UIManager : MonoBehaviour
{
    public static UIManager Instance => _instance;
    private static UIManager _instance;

    private void Awake()
    {
        _instance = this;
    }

    //====================================================================================================================//

    public DragController DragController
    {
        get
        {
            if (_dragController == null)
                _dragController = GetComponent<DragController>();

            return _dragController;
        }
    }
    private DragController _dragController;

    private new Transform transform;

    private void Start()
    {
        transform = gameObject.transform;
    }

    /*// Dragging
    //====================================================================================================================//

    public bool isDragging { get; private set; }
    private RectTransform _dragging;

    [SerializeField]
    private RectTransform placementPreviewTransform;

    public void OnDragStarted(RectTransform dragging)
    {
        _dragging = dragging;
        isDragging = true;
        
        _dragging.SetParent(transform);

        ForceUpdateLayouts();
        UpdateCommandList();
        placementPreviewTransform.gameObject.SetActive(true);
        
    }

    public void OnDrag(Vector2 mousePosition)
    {
        if (!isDragging)
            return;
        
        _dragging.position = mousePosition;

        var previewPos = FindClosestPreviewLocation(mousePosition);
        placementPreviewTransform.position = previewPos;
    }

    public void OnDragCompleted()
    {
        _dragging = null;
        isDragging = false;
        placementPreviewTransform.gameObject.SetActive(false);
    }

    [SerializeField]
    private List<CommandElementBase> _commandElementBases;

    [SerializeField]
    private List<Vector2> previewPositions;

    public Vector2 currentPos;
    private void UpdateCommandList()
    {
        previewPositions = new List<Vector2>();
        _commandElementBases = GetComponentsInChildren<CommandElementBase>().Where(x => x.transform != _dragging).ToList();
        
        UpdatePositionList();
    }

    private void UpdatePositionList()
    {
        foreach (var elementBase in _commandElementBases)
        {
            var pos = elementBase.transform.position;
            previewPositions.Add(pos);

            //Add the loop interior position
            if (!(elementBase is LoopCommandElement)) 
                continue;
            
            var offset = elementBase.transform.sizeDelta.y / 2f;
            var loopPos = new Vector2(offset, -offset);
            previewPositions.Add(elementBase.transform.TransformPoint(loopPos));
        }

        //Add the position at the bottom of the list
        var last = _commandElementBases[_commandElementBases.Count - 1].transform;
        var localPos = (Vector3) (last.sizeDelta * Vector3.down);
        
        previewPositions.Add(last.TransformPoint(localPos));
    }

    private Vector2 FindClosestPreviewLocation(Vector2 pos)
    {
        var y = pos.y;
        var shortestDist = 999f;
        var closestPos = Vector2.zero;

        foreach (var previewPosition in previewPositions)
        {
            var dist = Mathf.Abs(y - previewPosition.y);
            if(dist >= shortestDist)
                continue;

            shortestDist = dist;
            closestPos = previewPosition;
        }

        currentPos = closestPos;

        return closestPos;
    }*/

    //====================================================================================================================//

    public static void ForceUpdateLayouts()
    {
        var layouts = Instance.GetComponentsInChildren<LayoutGroup>();

        foreach (var layoutGroup in layouts)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.transform as RectTransform);
        }
    }
}
