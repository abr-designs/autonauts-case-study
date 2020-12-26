using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBot : MonoBehaviour, IStoreTarget
{
    public Transform StoredTarget { get; set; }

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
            new MoveCommand(transform, testTarget1, speed),
            new FixedLoopCommand(2, new []
            {
                new MoveCommand(transform, testTarget2, speed),
                new MoveCommand(transform, testTarget3, speed),
            }),
            new SearchCommand(ObjectManager, transform, this, new Vector3(-19.7f,0,9.3f), 10f),
            new MoveToStoredTargetCommand(transform, this, speed),
        });
    }

    public void Update()
    {
        _command?.MoveNext();
    }

    //====================================================================================================================//
    
    /*private void OnDrawGizmos()
    {
        if (transform == null)
            return;
        
        Gizmos.color = Color.green;
        
        Gizmos.DrawWireSphere(new Vector3(-19.7f,0,9.3f), 10f);
    }*/

}
