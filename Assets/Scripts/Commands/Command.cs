using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : IEnumerator
{
    public object Current { get; private set; }
    
    public bool MoveNext()
    {
        return true;
    }

    public void Reset()
    {

    }

    public void Dispose()
    {
    }
}
