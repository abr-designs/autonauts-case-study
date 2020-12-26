using UnityEngine;

public interface IInteractable
{
    public Transform transform { get;}
    public GameObject gameObject { get;}
    
    void Interact();
}
