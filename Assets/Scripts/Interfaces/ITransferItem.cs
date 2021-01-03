using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransferItem
{
    ItemData? heldItem { get; }

    bool TryPickupItem(ItemData item);
    
    //TODO Need to include a Transfer?
    
    (ItemData? item, int count) DropItems();
    ItemData? DropItems(int count);
}
