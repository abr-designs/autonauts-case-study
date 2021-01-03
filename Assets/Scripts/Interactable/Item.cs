using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable, IRecordAction
{
    public ItemData ItemData => itemData;

    [SerializeField]
    protected ItemData itemData;
    
    public new Transform transform { get; private set; }

    //Unity Functions
    //====================================================================================================================//
    
    private void Start()
    {
        transform = gameObject.transform;
    }
    
    public void OnMouseDown()
    {
        if (!ActionRecorder.CanSelectItems)
            return;

        var selectedBot = UIManager.Instance.selectedBot;
        var botTransform = selectedBot.transform;
        
        //left click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //TODO Pick-up Item
            ActionRecorder.RecordActions(new ICommand[]
            {
                new SearchCommand(botTransform, selectedBot, ItemData, transform.position, 20f),
                new MoveToStoredTargetCommand(botTransform, selectedBot, selectedBot.Speed, ItemData.Name),
                new InteractableCommand(botTransform, selectedBot,false, selectedBot, itemData.Name, InteractableCommand.TYPE.ITEM)
            });
        }
        //right click
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //TODO Drop Item Here
        }
    }

    //====================================================================================================================//
    
    public void Interact(in ITransferItem iTransferItem, bool useAlt)
    {
        iTransferItem.TryPickupItem(itemData);
    }

    //====================================================================================================================//

    

    //====================================================================================================================//


}
