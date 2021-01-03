using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCommand : TargetCommandBase
{
    public enum TYPE
    {
        ITEM,
        BUILDING
    }
    public string TargetName => _targetName;
    public bool UseAltInteraction => _useAltInteraction;
    public TYPE Type => _type;

    private readonly string _targetName;
    private readonly Bot _bot;
    private readonly bool _useAltInteraction;
    private readonly TYPE _type;
    
    
    public InteractableCommand(Transform moving, IStoreTarget iStoreTarget, bool useAltInteraction, Bot bot, string targetName, TYPE type) : base(moving, iStoreTarget)
    {
        _bot = bot;
        _useAltInteraction = useAltInteraction;
        _targetName = targetName;
        _type = type;
    }

    public override bool MoveNext()
    {
        UIManager.Instance.HighlightCommandElement(ID);
        
        if (IStoreTarget.StoredTarget is null)
            return false;

        if (Vector3.Distance(IStoreTarget.StoredTarget.transform.position, Moving.position) >= 0.5f)
            return false;
        
        //Interact, and clear target
        IStoreTarget.StoredTarget.Interact(_bot, _useAltInteraction);
        
        return true;
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}
