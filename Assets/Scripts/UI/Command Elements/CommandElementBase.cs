using System;
using UnityEngine;

public abstract class CommandElementBase : MonoBehaviour
{
    public new RectTransform transform { get; private set; }

    protected void Awake()
    {
        transform = gameObject.transform as RectTransform;
    }

    public abstract ICommand GenerateCommand();

    public virtual CommandElementBase GetParentGroup()
    {
        return GetComponentInParent<CommandElementBase>();
    }
}
