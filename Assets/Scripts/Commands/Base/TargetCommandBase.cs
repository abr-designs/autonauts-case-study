using UnityEngine;

public abstract class TargetCommandBase : ICommand
{
    public int ID { get; set; }

    public IInteractable StoredTarget => IStoreTarget?.StoredTarget;

    protected readonly Transform Moving;
    protected readonly IStoreTarget IStoreTarget;

    protected TargetCommandBase(Transform moving, IStoreTarget iStoreTarget)
    {
        Moving = moving;
        IStoreTarget = iStoreTarget;
    }
    
    
    public abstract bool MoveNext();

    public abstract void Reset();
}
