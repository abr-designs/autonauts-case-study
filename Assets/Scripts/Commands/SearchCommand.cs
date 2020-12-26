using System.Collections.Generic;
using UnityEngine;

//FIXME This needs to store a reference, but also be able to be called using regular MoveNext
public class SearchCommand : ICommand
{
    private readonly Transform _moving;
    private readonly IStoreTarget _iStoreTarget;
    private readonly ObjectManager _objectManager;
    private readonly Vector3 _searchLocation;
    private readonly float _radius;
    
    public SearchCommand(ObjectManager objectManager,Transform moving, IStoreTarget iStoreTarget, Vector3 searchLocation, float searchRadius)
    {
        _moving = moving;
        
        _searchLocation = searchLocation;
        _radius = searchRadius;
        _objectManager = objectManager;

        _iStoreTarget = iStoreTarget;
    }
    
    public bool MoveNext()
    {
        var objects = _objectManager.objects.ToArray();

        if (objects.Length <= 0)
            return false;

        //Find all the objects that are legal targets
        var possibleOptions = new List<Transform>();

        foreach (var t in objects)
        {
            var pos = t.position;

            var dist = Vector3.Distance(_searchLocation, pos);
            
            if (dist > _radius)
                continue;
            
            possibleOptions.Add(t);
        }

        if (possibleOptions.Count <= 0)
            return false;
        
        //Find the closest object to the actor
        var shortestIndex = -1;
        var shortestDist = 999f;
        
        for (int i = 0; i < possibleOptions.Count; i++)
        {
            var pos = possibleOptions[i].position;

            var dist = Vector3.Distance(_moving.position, pos);
            
            if(dist >= shortestDist)
                continue;

            shortestDist = dist;
            shortestIndex = i;
        }

        //If no object was found, return false
        if (shortestIndex < 0) 
            return false;
        
        _iStoreTarget.StoredTarget = possibleOptions[shortestIndex];
        Debug.DrawLine(_moving.position, _iStoreTarget.StoredTarget.position, Color.cyan, 1f);

        return true;
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}
