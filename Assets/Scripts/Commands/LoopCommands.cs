using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InfiniteLoopCommand :LoopCommandBase, ICommand
{
    public InfiniteLoopCommand(IEnumerable<ICommand> internalCommands) : base (internalCommands)
    {
    }
    
    public bool MoveNext()
    {
        //Call the current command until it gives the green light to move to the next index
        if (!_internalCommands[_currentIndex].MoveNext())
            return false;

        if (_currentIndex + 1 >= _internalCommands.Length)
            _currentIndex = 0;
        else _currentIndex++;

        return false;
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}

public class FixedLoopCommand : LoopCommandBase, ICommand
{
    public int Count => _count;

    private int _currentCount;
    private readonly int _count;
    
    public FixedLoopCommand(IEnumerable<ICommand> internalCommands, int count) : base(internalCommands) 
    {
        _count = count;
    }
    
    public bool MoveNext()
    {
        //Call the current command until it gives the green light to move to the next index
        if (!_internalCommands[_currentIndex].MoveNext())
            return false;

        //If we reached the end of the loop, add one to the loop count and reset the current selected index
        if (_currentIndex + 1 >= _internalCommands.Length)
        {
            _currentCount++;
            _currentIndex = 0;
        }
        else
        {
            _currentIndex++;
        }

        //keep looping until we reach the threshold of _count
        if (_currentCount < _count) 
            return false;
        
        //Clean this up before we leave, so that if/when we return its ready
        Reset();
        
        return true;

    }

    public void Reset()
    {
        _currentCount = 0;
        _currentIndex = 0;
    }
}

public class ConditionalLoopCommand :LoopCommandBase, ICommand
{
    public CONDITION Condition => _condition;

    private readonly IConditional _iConditional;
    private readonly CONDITION _condition;
    public ConditionalLoopCommand(IEnumerable<ICommand> internalCommands, IConditional iConditional, CONDITION condition) : base (internalCommands)
    {
        _iConditional = iConditional;
        _condition = condition;

    }
    public bool MoveNext()
    {
        if (_iConditional.MeetsCondition(_condition))
            return true;
        
        //Call the current command until it gives the green light to move to the next index
        if (!_internalCommands[_currentIndex].MoveNext())
            return false;

        if (_currentIndex + 1 >= _internalCommands.Length)
            _currentIndex = 0;
        else _currentIndex++;

        return false;
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}