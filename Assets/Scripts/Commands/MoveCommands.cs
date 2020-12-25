using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoveCommands
{
    public static IEnumerator MoveToCoroutine(MonoBehaviour monoBehaviour, Transform target, float speed)
    {
        var mover = monoBehaviour.transform;
        var dist = 999f;
        while (dist > 0.5f)
        {
            var targetPosition = target.position;
            var currentPosition = mover.position;
            
            
            currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

            dist = Vector3.Distance(currentPosition, targetPosition);

            mover.position = currentPosition;
            
            yield return null;
        }
    }
    public static IEnumerator MoveToCoroutine(MonoBehaviour monoBehaviour, Vector3 targetLocation, float speed)
    {
        throw new NotImplementedException();
    }
    public static IEnumerator MoveToCoroutine(MonoBehaviour monoBehaviour/*, Interactable*/, float speed)
    {
        throw new NotImplementedException();
    }
}
