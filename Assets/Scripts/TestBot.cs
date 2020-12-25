using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBot : MonoBehaviour
{
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
            new FixedLoopCommand(3, new []
            {
                new MoveCommand(transform, testTarget2, speed),
                new MoveCommand(transform, testTarget3, speed),
            })
        });
    }

    public void Update()
    {
        _command?.MoveNext();
    }
}
