using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public ItemData ItemData => itemData;

    [SerializeField]
    protected ItemData itemData;
    
    public new Transform transform { get; private set; }

    private void Start()
    {
        transform = gameObject.transform;
    }

    public void Interact(in ITransferItem iTransferItem)
    {
        iTransferItem.TryPickupItem(itemData);
    }
}
