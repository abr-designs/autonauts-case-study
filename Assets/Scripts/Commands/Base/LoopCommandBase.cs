using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class LoopCommandBase
{
    protected readonly ICommand[] _internalCommands;
    protected int _currentIndex;
    
    public LoopCommandBase(IEnumerable<ICommand> internalCommands)
    {
        _internalCommands = internalCommands.ToArray();
        _currentIndex = 0;
    }
}
