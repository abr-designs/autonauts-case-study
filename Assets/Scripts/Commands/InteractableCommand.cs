using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCommand : TargetCommandBase
{
    private readonly Bot _bot;
    private readonly bool _useAltInteraction;
    
    
    public InteractableCommand(Transform moving, IStoreTarget iStoreTarget, bool useAltInteraction, Bot bot) : base(moving, iStoreTarget)
    {
        _bot = bot;
        _useAltInteraction = useAltInteraction;
    }

    public override bool MoveNext()
    {
        if (IStoreTarget.StoredTarget is null)
            return false;

        if (Vector3.Distance(IStoreTarget.StoredTarget.transform.position, Moving.position) >= 0.5f)
            return false;
        
        //Interact, and clear target
        IStoreTarget.StoredTarget.Interact(_bot, _useAltInteraction);
        IStoreTarget.StoredTarget = null;
        
        return true;
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}
