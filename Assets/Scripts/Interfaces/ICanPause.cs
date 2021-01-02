using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanPause
{
    bool IsPaused { get; }

    void SetPaused(bool paused);
}
