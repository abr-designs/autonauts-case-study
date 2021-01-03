using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IInteractable, IHoldItems, IRecordAction
{
    public string Name;
    
    //IHoldItems Properties
    //====================================================================================================================//
    
    public ItemData? heldItem { get; }
    public int heldItems { get; }
    public int itemCapacity => 10;

    //Unity Functions
    //====================================================================================================================//
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMouseDown()
    {
        if (!ActionRecorder.CanSelectBuildings)
            return;

        if (ActionRecorder.SelectingTarget)
        {
            ActionRecorder.SetBuildingTarget(this);
            return;
        }
        
        var selectedBot = UIManager.Instance.selectedBot;
        var botTransform = selectedBot.transform;
        
        //left click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //TODO Take Item
            ActionRecorder.RecordActions(new ICommand[]
            {
                new StoreAndMoveToStoredTargetCommand(botTransform, selectedBot, this, selectedBot.Speed),
                new InteractableCommand(botTransform, selectedBot, false, selectedBot)
            });
        }
        //right click
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //TODO Give Item Here
            ActionRecorder.RecordActions(new ICommand[]
            {
                new StoreAndMoveToStoredTargetCommand(botTransform, selectedBot, this, selectedBot.Speed),
                new InteractableCommand(botTransform, selectedBot, true, selectedBot)
            });
        }
    }
    
    //====================================================================================================================//
    
    public void Interact(in ITransferItem iTransferItem, bool useAlt)
    {
        if (useAlt)
        {
            //TODO Try Store Item
            return;
        }
        
        //TODO Try Take Item
    }



}
