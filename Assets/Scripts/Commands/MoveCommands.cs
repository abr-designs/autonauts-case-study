using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private readonly Transform _moving;
    
    private readonly Transform _target;
    private readonly float _speed;
    
    public MoveCommand(Transform moving, Transform target, float speed)
    {
        _moving = moving;
        
        _target = target;

        _speed = speed;
    }
    
    public bool MoveNext()
    {
        var currentPosition = _moving.position;
        var targetPosition = _target.position;

        if (Vector3.Distance(currentPosition, targetPosition) <= 0.5)
            return true;
        
        
        currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, _speed * Time.deltaTime);
        
        _moving.position = currentPosition;

        return false;
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}
