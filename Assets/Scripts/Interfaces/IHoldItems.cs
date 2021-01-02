using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldItems
{
    ItemData? heldItem { get; }
    int heldItems { get; }
    int itemCapacity { get; }
}
