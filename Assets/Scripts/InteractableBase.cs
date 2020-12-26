using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    public new Transform transform { get; private set; }

    private void Start()
    {
        transform = gameObject.transform;
    }

    public void Interact()
    {
        Destroy(gameObject);
    }
}
