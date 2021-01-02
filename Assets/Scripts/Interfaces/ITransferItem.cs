using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransferItem
{
    void TryPickupItem(ItemData item);
    (ItemData? item, int count) DropItems();
}
