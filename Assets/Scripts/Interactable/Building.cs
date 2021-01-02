using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IInteractable, IHoldItems
{
    public string Name;
    
    //IHoldItems Properties
    //====================================================================================================================//
    
    public ItemData? heldItem { get; }
    public int heldItems { get; }
    public int itemCapacity { get; }

    //Unity Functions
    //====================================================================================================================//
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //====================================================================================================================//
    
    public void Interact(in ITransferItem iTransferItem)
    {
        throw new System.NotImplementedException();
    }


}
