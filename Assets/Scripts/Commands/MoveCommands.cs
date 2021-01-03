using UnityEngine;

public class MoveCommand : ICommand
{
    public int ID { get; set; }

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
        UIManager.Instance.HighlightCommandElement(ID);
        
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
public class MoveToPositionCommand : ICommand
{
    public int ID { get; set; }
    
    public Vector3 Target => _target;

    private readonly Transform _moving;
    
    private readonly Vector3 _target;
    private readonly float _speed;
    
    public MoveToPositionCommand(Transform moving, Vector3 target, float speed)
    {
        _moving = moving;
        
        _target = target;

        _speed = speed;
    }
    
    public bool MoveNext()
    {
        UIManager.Instance.HighlightCommandElement(ID);
        
        var currentPosition = _moving.position;
        //var targetPosition = _target.position;

        if (Vector3.Distance(currentPosition, _target) <= 0.5)
            return true;

        Debug.DrawLine(currentPosition, _target, Color.green);
        
        
        currentPosition = Vector3.MoveTowards(currentPosition, _target, _speed * Time.deltaTime);
        
        _moving.position = currentPosition;

        return false;
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}
public class MoveToStoredTargetCommand : TargetCommandBase
{
    public string TargetName { get;}
    
    private readonly float _speed;
    
    public MoveToStoredTargetCommand(Transform moving, IStoreTarget iStoreTarget, float speed, string targetName) : base(moving, iStoreTarget)
    {
        _speed = speed;
        TargetName = targetName;
    }
    
    public override bool MoveNext()
    {
        UIManager.Instance.HighlightCommandElement(ID);
        
        var currentPosition = Moving.position;
        var targetPosition = IStoreTarget.StoredTarget.transform.position;

        if (Vector3.Distance(currentPosition, targetPosition) <= 0.5)
            return true;

        Debug.DrawLine(currentPosition, targetPosition, Color.green);
        
        
        currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, _speed * Time.deltaTime);
        
        Moving.position = currentPosition;

        return false;
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}
public class StoreAndMoveToStoredTargetCommand : TargetCommandBase
{
    public IInteractable TargetInteractable { get; }
    private readonly float _speed;
    
    public StoreAndMoveToStoredTargetCommand(Transform moving, IStoreTarget iStoreTarget, IInteractable targetInteractable, float speed) : base(moving, iStoreTarget)
    {
        _speed = speed;
        IStoreTarget.StoredTarget = targetInteractable;
        TargetInteractable = targetInteractable;
    }
    
    public override bool MoveNext()
    {
        UIManager.Instance.HighlightCommandElement(ID);
        
        var currentPosition = Moving.position;
        var targetPosition = TargetInteractable.transform.position;

        if (Vector3.Distance(currentPosition, targetPosition) <= 0.5)
        {
            IStoreTarget.StoredTarget = TargetInteractable;
            return true;
        }

        Debug.DrawLine(currentPosition, targetPosition, Color.green);
        
        
        currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, _speed * Time.deltaTime);
        
        Moving.position = currentPosition;

        return false;
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}
