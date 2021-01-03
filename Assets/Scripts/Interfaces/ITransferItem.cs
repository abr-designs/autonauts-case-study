using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransferItem
{
    void TryPickupItem(ItemData item);
    
    //TODO Need to include a Transfer?
    
    (ItemData? item, int count) DropItems();
}
