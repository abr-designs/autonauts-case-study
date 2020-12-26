using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBot : MonoBehaviour, IStoreTarget, IConditional
{
    [SerializeField]
    private int heldItems;
    private int itemCapacity = 4;
    
    public IInteractable StoredTarget { get; set; }

    //====================================================================================================================//
    
    public ObjectManager ObjectManager;
    
    public Transform testTarget1;

    public Transform testTarget2;
    
    public Transform testTarget3;

    //The stored code will likely always need to be a collection
    private ICommand _command;

    public float speed = 5f;

    private new Transform transform;

    private void Start()
    {
        transform = gameObject.transform;
        _command = new InfiniteLoopCommand(new ICommand[]
        {
            new ConditionalLoopCommand(new ICommand[]
            {
                new SearchCommand(transform, this, ObjectManager, new Vector3(-19.7f, 0, 9.3f), 10f),
                new MoveToStoredTargetCommand(transform, this, speed),
                new InteractableCommand(transform, this, this)
                
            }, this, CONDITION.HANDS_FULL),
            
            new MoveCommand(transform, testTarget2, speed),
            
        });

        /*_command = new InfiniteLoopCommand(new ICommand[]
        {
            new MoveCommand(transform, testTarget1, speed),
            new FixedLoopCommand(new[]
            {
                new MoveCommand(transform, testTarget2, speed),
                new MoveCommand(transform, testTarget3, speed),
            }, 2),
            new SearchCommand(transform, this, ObjectManager, new Vector3(-19.7f, 0, 9.3f), 10f),
            new MoveToStoredTargetCommand(transform, this, speed),
            new InteractableCommand(transform, this, this)
        });*/
    }

    public void Update()
    {
        _command?.MoveNext();
    }

    //====================================================================================================================//

    public void TryPickupInteractable(int holdItem)
    {
        if (heldItems + holdItem > itemCapacity)
            return;

        heldItems += holdItem;
    }

    public bool MeetsCondition(CONDITION condition)
    {
        switch (condition)
        {
            case CONDITION.HANDS_FULL:
                return heldItems == itemCapacity;
            case CONDITION.HANDS_NOT_FULL:
                return heldItems != itemCapacity;
            case CONDITION.HANDS_EMPTY:
                return heldItems == 0;
            case CONDITION.HANDS_NOT_EMPTY:
                return heldItems != 0;
            default:
                throw new ArgumentOutOfRangeException(nameof(condition), condition, null);
        }
    }
}
