using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public bool isDragging { get; private set; }
    private RectTransform _dragging;

    [SerializeField]
    private RectTransform placementPreviewTransform;
    
    [SerializeField]
    private List<CommandElementBase> _commandElementBases;

    private List<Vector2> previewPositions;

    private new Transform transform;

    //Unity Functions
    //====================================================================================================================//
    
    private void Start()
    {
        transform = gameObject.transform;
        
        placementPreviewTransform.gameObject.SetActive(false);
    }

    //Dragging Functions
    //====================================================================================================================//
    
    public void OnDragStarted(RectTransform dragging)
    {
        _dragging = dragging;
        isDragging = true;
        
        _dragging.SetParent(transform);

        UIManager.ForceUpdateLayouts();
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

    //Calculation Functions
    //====================================================================================================================//
    
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
            if (!(elementBase is LoopCommandElement loopCommandElement)) 
                continue;

            UpdateLocationsForLoop(loopCommandElement);
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

        return closestPos;
    }

    private void UpdateLocationsForLoop(in LoopCommandElement loopCommandElement)
    {
        var loopTrans = loopCommandElement.transform;
        
        
        var offset = loopTrans.sizeDelta.y / 2f;
        var loopPos = new Vector2(offset, -offset);
        previewPositions.Add(loopTrans.TransformPoint(loopPos));
    }

    //====================================================================================================================//
    
}
