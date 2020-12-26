using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCommand : TargetCommandBase
{
    private readonly TestBot _testBot;
    
    
    public InteractableCommand(Transform moving, IStoreTarget iStoreTarget, TestBot testBot) : base(moving, iStoreTarget)
    {
        _testBot = testBot;
    }

    public override bool MoveNext()
    {
        if (IStoreTarget.StoredTarget is null)
            return false;

        if (Vector3.Distance(IStoreTarget.StoredTarget.transform.position, Moving.position) >= 0.5f)
            return false;
        
        //Interact, and clear target
        IStoreTarget.StoredTarget.Interact(_testBot);
        IStoreTarget.StoredTarget = null;
        
        return true;
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}
