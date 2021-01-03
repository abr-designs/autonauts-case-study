using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    public int ID { get; set; }
    public bool MoveNext();

    public void Reset();
}
