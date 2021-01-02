using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCommand : TargetCommandBase
{
    private readonly Bot bot;
    
    
    public InteractableCommand(Transform moving, IStoreTarget iStoreTarget, Bot bot) : base(moving, iStoreTarget)
    {
        this.bot = bot;
    }

    public override bool MoveNext()
    {
        if (IStoreTarget.StoredTarget is null)
            return false;

        if (Vector3.Distance(IStoreTarget.StoredTarget.transform.position, Moving.position) >= 0.5f)
            return false;
        
        //Interact, and clear target
        IStoreTarget.StoredTarget.Interact(bot);
        IStoreTarget.StoredTarget = null;
        
        return true;
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}
