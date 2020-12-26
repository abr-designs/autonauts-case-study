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

        Debug.DrawLine(currentPosition, targetPosition, Color.green);
        
        
        currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, _speed * Time.deltaTime);
        
        _moving.position = currentPosition;

        return false;
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}
public class MoveToStoredTargetCommand : ICommand
{
    private readonly Transform _moving;

    private readonly IStoreTarget _iStoreTarget;
    private readonly float _speed;
    
    public MoveToStoredTargetCommand(Transform moving, IStoreTarget iStoreTarget, float speed)
    {
        _moving = moving;

        _iStoreTarget = iStoreTarget;

        _speed = speed;
    }
    
    public bool MoveNext()
    {
        var currentPosition = _moving.position;
        var targetPosition = _iStoreTarget.StoredTarget.position;

        if (Vector3.Distance(currentPosition, targetPosition) <= 0.5)
            return true;

        Debug.DrawLine(currentPosition, targetPosition, Color.green);
        
        
        currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, _speed * Time.deltaTime);
        
        _moving.position = currentPosition;

        return false;
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}
