using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DragController : MonoBehaviour
{
    [Serializable]
    public struct MoveData
    {
        public enum DIR
        {
            NONE,
            ABOVE,
            BELOW
        }

        public Vector2 previewPosition;

        public Transform parent;
        public Transform siblingTransform;
        public DIR direction;

        public void SetSibling(in Transform transform)
        {
            if (direction == DIR.NONE)
            {
                transform.SetParent(parent);
                return;
            }
            
            transform.SetParent(siblingTransform.parent);

            var siblingIndex = siblingTransform.GetSiblingIndex();
            
            switch (direction)
            {
                case DIR.ABOVE:
                    transform.SetSiblingIndex(siblingIndex);
                    break;
                case DIR.BELOW:
                    transform.SetSiblingIndex(siblingIndex + 1);
                    break;
            }
        }
    }

    //====================================================================================================================//

    

    //====================================================================================================================//
    
    
    public bool isDragging { get; private set; }
    private RectTransform _dragging;
    private MoveData _currentMoveData;

    [SerializeField]
    private RectTransform placementPreviewTransform;

    [SerializeField]
    private RectTransform codeSectionRectTransform;
    
    //[SerializeField]
    private List<CommandElementBase> _commandElementBases;

    private List<MoveData> _previewData;

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
        SetCommandElementDragging(_dragging, isDragging);

        UIManager.ForceUpdateLayouts();
        UpdateCommandList();
        placementPreviewTransform.gameObject.SetActive(true);

        Highlight(_dragging);

    }

    public void OnDrag(Vector2 mousePosition)
    {
        if (!isDragging)
            return;
        
        _dragging.position = mousePosition;

        _currentMoveData = FindClosestPreviewLocation(mousePosition);
        placementPreviewTransform.position = _currentMoveData.previewPosition;
    }

    public void OnDragCompleted()
    {
        ResetColor(_dragging);
        SetCommandElementDragging(_dragging, isDragging);
        _currentMoveData.SetSibling(_dragging);
        
        
        _dragging = null;
        isDragging = false;
        placementPreviewTransform.gameObject.SetActive(false);
        
        UIManager.ForceUpdateLayouts();
        
    }

    private void SetCommandElementDragging(in Transform target, bool isDragging)
    {
        var temps = target.GetComponentsInChildren<CommandElementBase>();
        foreach (var commandElementBase in temps)
        {
            commandElementBase.isDragging = isDragging;
        }
    }

    //Calculation Functions
    //====================================================================================================================//
    
    private void UpdateCommandList()
    {
        _previewData = new List<MoveData>();
        _commandElementBases = GetComponentsInChildren<CommandElementBase>().Where(x => x.transform != _dragging).ToList();
        
        UpdatePositionList();
    }

    private void UpdatePositionList()
    {
        if (_commandElementBases.Count == 0)
        {
            _previewData.Add(new MoveData
            {
                previewPosition = codeSectionRectTransform.position,
                direction = MoveData.DIR.NONE,
                parent = codeSectionRectTransform
            });
            
            return;
        }
        
        foreach (var elementBase in _commandElementBases)
        {
            var pos = elementBase.transform.position;
            _previewData.Add(new MoveData
            {
                previewPosition = pos,
                siblingTransform = elementBase.transform,
                direction = MoveData.DIR.ABOVE
            });

            //Add the loop interior position
            if (!(elementBase is LoopCommandElement loopCommandElement)) 
                continue;

            UpdateLocationsForLoop(loopCommandElement);
        }

        //Add the position at the bottom of the list
        var last = _commandElementBases[_commandElementBases.Count - 1].transform;
        var localPos = (Vector3) (last.sizeDelta * Vector3.down);
        
        _previewData.Add(new MoveData
        {
            previewPosition = last.TransformPoint(localPos),
            siblingTransform = last,
            direction = MoveData.DIR.BELOW
        });
    }

    private MoveData FindClosestPreviewLocation(Vector2 pos)
    {
        var y = pos.y;
        var shortestDist = 999f;
        var closest = new MoveData();

        foreach (var moveData in _previewData)
        {
            var dist = Mathf.Abs(y - moveData.previewPosition.y);
            if(dist >= shortestDist)
                continue;

            shortestDist = dist;
            closest = moveData;
        }

        return closest;
    }

    private void UpdateLocationsForLoop(in LoopCommandElement loopCommandElement)
    {
        var loopTrans = loopCommandElement.transform;
        var cmb = loopTrans.GetChild(loopTrans.childCount - 1).GetComponent<CommandElementBase>();

        if (loopTrans.childCount > 2 && !(cmb is null) && cmb.gameObject.activeInHierarchy)
        {
            //Add the position at the bottom of the list
            var last = cmb.transform;
            var localPos = (Vector3) (last.sizeDelta * Vector3.down);

            _previewData.Add(new MoveData
            {
                previewPosition = last.TransformPoint(localPos),
                siblingTransform = last,
                direction = MoveData.DIR.BELOW
            });

            return;
        }

        var offset = loopTrans.sizeDelta.y / 2f;
        var loopPos = new Vector2(offset, -offset);
        _previewData.Add(new MoveData
        {
            previewPosition = loopTrans.TransformPoint(loopPos),
            parent = loopTrans,
            direction = MoveData.DIR.NONE
        });

    }

    //Color States
    //====================================================================================================================//

    private void Highlight(in RectTransform rectTransform)
    {
        var elements = rectTransform.GetComponentsInChildren<CommandElementBase>();
        foreach (var elementBase in elements)
        {
            elementBase.LightenColor();
        }
    }

    private void ResetColor(in RectTransform rectTransform)
    {
        var elements = rectTransform.GetComponentsInChildren<CommandElementBase>();
        foreach (var elementBase in elements)
        {
            elementBase.ResetColor();
        }
    }

    //====================================================================================================================//
    
}
