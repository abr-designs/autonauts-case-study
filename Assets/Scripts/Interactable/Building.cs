using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IInteractable, IHoldItems, IRecordAction, IConditional
{
    public string Name;
    
    //IHoldItems Properties
    //====================================================================================================================//
    
    public ItemData? heldItem { get; private set; }
    public int heldItems { get; private set; }
    public int itemCapacity => 10;

    //Unity Functions
    //====================================================================================================================//
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMouseOver()
    {
        if (!ActionRecorder.CanSelectBuildings)
            return;
        
        var selectedBot = UIManager.Instance.selectedBot;
        var botTransform = selectedBot.transform;
        
        //left click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (ActionRecorder.SelectingTarget)
            {
                ActionRecorder.SetBuildingTarget(this);
                return;
            }
            
            //TODO Take Item
            ActionRecorder.RecordActions(new ICommand[]
            {
                new StoreAndMoveToStoredTargetCommand(botTransform, selectedBot, this, selectedBot.Speed),
                new InteractableCommand(botTransform, selectedBot, false, selectedBot, Name, InteractableCommand.TYPE.BUILDING)
            });
        }
        //right click
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //TODO Give Item Here
            ActionRecorder.RecordActions(new ICommand[]
            {
                new StoreAndMoveToStoredTargetCommand(botTransform, selectedBot, this, selectedBot.Speed),
                new InteractableCommand(botTransform, selectedBot, true, selectedBot, Name, InteractableCommand.TYPE.BUILDING)
            });
        }
    }
    
    //====================================================================================================================//
    
    public void Interact(in ITransferItem iTransferItem, bool useAlt)
    {
        var item = heldItem.GetValueOrDefault();
        //Add item to building
        if (useAlt)
        {
            var potentialItem = iTransferItem.heldItem;

            if (!potentialItem.HasValue)
                return;

            if (heldItem.HasValue && !potentialItem.Value.Equals(item))
                return;
            
            if(heldItems + potentialItem.Value.Space > itemCapacity)
                return;
            
            //TODO Try Store Item
            var toTransfer = iTransferItem.DropItems(1);

            if (!heldItem.HasValue)
                Name = $"{toTransfer.Value.Name} Storage";

            heldItem = toTransfer;
            heldItems += item.Space;
            
            return;
        }
        
        //Take Item from building
        //TODO Try Take Item
        if (!heldItem.HasValue)
            return;
        
        if(heldItems < item.Space)
            return;

        iTransferItem.TryPickupItem(item);
        heldItems -= item.Space;
    }


    public bool MeetsCondition(CONDITION condition)
    {
        switch (condition)
        {
            case CONDITION.BUILDING_FULL:
                return heldItems == itemCapacity;
            case CONDITION.BUILDING_NOT_FULL:
                return heldItems != itemCapacity;
            case CONDITION.BUILDING_EMPTY:
                return heldItems == 0;
            case CONDITION.BUILDING_NOT_EMPTY:
                return heldItems != 0;
            default:
                throw new ArgumentOutOfRangeException(nameof(condition), condition, null);
        }
    }
}
