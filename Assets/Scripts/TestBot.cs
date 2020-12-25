using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBot : MonoBehaviour
{
    public Transform testTarget1;

    public Transform testTarget2;

    public float speed = 5f;

    private IEnumerator _command;
    
    // Start is called before the first frame update
    private void Start()
    {
        _command = LoopCommands.LoopCoroutine(this, new LoopData
        {
            Type = LoopData.TYPE.FOREVER
        }, new List<IEnumerator>
        {
            MoveCommands.MoveToCoroutine(this, testTarget1,speed),
            MoveCommands.MoveToCoroutine(this, testTarget2,speed),
        });

        StartCoroutine(_command);
    }

}
