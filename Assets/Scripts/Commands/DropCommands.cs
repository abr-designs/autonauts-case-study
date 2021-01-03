using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCommand : ICommand
{
    public int ID { get; set; }

    private readonly ITransferItem _iTransferItem;

    public DropCommand(ITransferItem iTransferItem)
    {
        _iTransferItem = iTransferItem;
    }


    public bool MoveNext()
    {
        UIManager.Instance.HighlightCommandElement(ID);
        
        //TODO Get items to drop
       var toDrop =  _iTransferItem.DropItems();
       
       //TODO Spawn Items to drop here

        return true;
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}
