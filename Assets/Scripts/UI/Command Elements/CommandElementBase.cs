using UnityEngine;

public abstract class CommandElementBase : MonoBehaviour
{
    public abstract ICommand GenerateCommand();

    public virtual CommandElementBase GetParentGroup()
    {
        return GetComponentInParent<CommandElementBase>();
    }
}
