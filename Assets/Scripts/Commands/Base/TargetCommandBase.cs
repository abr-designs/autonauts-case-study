using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetCommandBase : ICommand
{
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
