using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public static class LoopCommands
{
    
    public static IEnumerator LoopCoroutine(MonoBehaviour monoBehaviour, LoopData loopData, List<IEnumerator> internalCommands)
    {
        switch (loopData.Type)
        {
            //--------------------------------------------------------------------------------------------------------//
            case LoopData.TYPE.FOREVER:
                while (true)
                {
                    foreach (var internalCommand in internalCommands)
                    {
                        yield return monoBehaviour.StartCoroutine(internalCommand);
                    }
                }
                break;
            //--------------------------------------------------------------------------------------------------------//
            case LoopData.TYPE.COUNT:
                for (var i = 0; i < loopData.Count; i++)
                {
                    foreach (var internalCommand in internalCommands)
                    {
                        yield return monoBehaviour.StartCoroutine(internalCommand);
                    }
                }
                break;
            //--------------------------------------------------------------------------------------------------------//
            case LoopData.TYPE.CONDITION:
                throw new NotImplementedException();
            //--------------------------------------------------------------------------------------------------------//
            default:
                throw new ArgumentOutOfRangeException(nameof(loopData.Type), loopData.Type, null);
        }
    }
}*/
