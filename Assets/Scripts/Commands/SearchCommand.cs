using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//FIXME This needs to store a reference, but also be able to be called using regular MoveNext
public class SearchCommand : TargetCommandBase
{
    public ItemData ItemData => _itemData;
    public Vector3 SearchLocation => _searchLocation;
    public float Radius => _radius;


    private static ObjectManager _objectManager;
    
    private readonly ItemData _itemData;
    private readonly Vector3 _searchLocation;
    private readonly float _radius;
    
    public SearchCommand(Transform moving, IStoreTarget iStoreTarget, ItemData itemData, Vector3 searchLocation, float searchRadius) : base (moving, iStoreTarget)
    {
        if(!_objectManager)
            _objectManager = Object.FindObjectOfType<ObjectManager>();
        
        _searchLocation = searchLocation;
        _radius = searchRadius;
        _itemData = itemData;
    }
    
    public override bool MoveNext()
    {
        UIManager.Instance.HighlightCommandElement(ID);
        
        var objects = _objectManager.Items.Where(x => x.ItemData.Equals(_itemData)).ToArray();

        if (objects.Length <= 0)
            return false;

        //Find all the objects that are legal targets
        var possibleOptions = new List<IInteractable>();

        foreach (var interactable in objects)
        {
            if (interactable == null)
                continue;
            
            var pos = interactable.transform.position;

            var dist = Vector3.Distance(_searchLocation, pos);
            
            if (dist > _radius)
                continue;
            
            possibleOptions.Add(interactable);
        }

        if (possibleOptions.Count <= 0)
            return false;
        
        //Find the closest object to the actor
        var shortestIndex = -1;
        var shortestDist = 999f;
        
        for (int i = 0; i < possibleOptions.Count; i++)
        {
            var pos = possibleOptions[i].transform.position;

            var dist = Vector3.Distance(Moving.position, pos);
            
            if(dist >= shortestDist)
                continue;

            shortestDist = dist;
            shortestIndex = i;
        }

        //If no object was found, return false
        if (shortestIndex < 0) 
            return false;
        
        IStoreTarget.StoredTarget = possibleOptions[shortestIndex];
        Debug.DrawLine(Moving.position, IStoreTarget.StoredTarget.transform.position, Color.cyan, 1f);

        return true;
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}
