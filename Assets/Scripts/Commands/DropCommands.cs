using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCommand : ICommand
{
    private readonly ITransferItem _iTransferItem;

    public DropCommand(ITransferItem iTransferItem)
    {
        _iTransferItem = iTransferItem;
    }

    public bool MoveNext()
    {
        _iTransferItem.DropItems();

        return true;
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}
