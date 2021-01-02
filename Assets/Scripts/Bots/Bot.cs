using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour, IStoreTarget, IConditional, IHoldItems, ICanPause, ITransferItem
{
    //IHoldItems Properties
    //====================================================================================================================//

    public ItemData? heldItem { get; private set; }
    public int heldItems { get; private set; }
    public int itemCapacity => 4;

    private int _itemCount;

    //ICanPause Properties
    //====================================================================================================================//

    public bool IsPaused { get; private set; } = true;

    //IStoreTarget Properties
    //====================================================================================================================//
    
    public IInteractable StoredTarget { get; set; }

    //====================================================================================================================//
    protected static ObjectManager ObjectManager;
    
    public float Speed => speed;

    [SerializeField]
    protected float speed = 5f;
    
    public ICommand[] Command => _command;
    //The stored code will likely always need to be a collection
    private ICommand[] _command;
    private int currentIndex;

    //Unit Functions
    //====================================================================================================================//
    
    // Start is called before the first frame update
    private void Start()
    {
        if (ObjectManager == null)
            ObjectManager = FindObjectOfType<ObjectManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        ProcessStep();
    }

    private void OnMouseDown()
    {
        UIManager.Instance.SetCodeWindowActive(true, this);
    }

    //====================================================================================================================//

    private void ProcessStep()
    {
        if (IsPaused)
            return;

        if (_command == null || _command.Length == 0)
            return;
        
        if (!_command[currentIndex].MoveNext())
            return;
        
        if (currentIndex + 1 >= _command.Length)
            currentIndex = 0;
        else currentIndex++;
    }

    //IConditional Functions
    //====================================================================================================================//
    
    public bool MeetsCondition(CONDITION condition)
    {
        switch (condition)
        {
            case CONDITION.HANDS_FULL:
                return heldItems == itemCapacity;
            case CONDITION.HANDS_NOT_FULL:
                return heldItems != itemCapacity;
            case CONDITION.HANDS_EMPTY:
                return heldItems == 0;
            case CONDITION.HANDS_NOT_EMPTY:
                return heldItems != 0;
            default:
                throw new ArgumentOutOfRangeException(nameof(condition), condition, null);
        }
    }

    //ICanPause Functions
    //====================================================================================================================//
    
    public void SetPaused(bool paused)
    {
        IsPaused = paused;
    }

    //ITransferItem Functions
    //====================================================================================================================//
    
    public void TryPickupItem(ItemData item)
    {
        if (heldItem.HasValue && !heldItem.Value.Name.Equals(item.Name))
            return;
        
        if (heldItems + item.Space > itemCapacity)
            return;

        heldItems += item.Space;
        heldItem = item;
        _itemCount++;
    }

    public (ItemData? item, int count) DropItems()
    {
        if (!heldItem.HasValue)
            return (null, 0);

        var outData = (heldItem, _itemCount);
        heldItem = null;
        _itemCount = 0;
        heldItems = 0;

        return outData;
    }

    //====================================================================================================================//
    
}
