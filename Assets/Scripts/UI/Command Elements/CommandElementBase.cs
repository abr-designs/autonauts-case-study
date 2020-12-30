using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class CommandElementBase : MonoBehaviour
{
    public abstract ICommand GenerateCommand();

    public abstract CommandElementBase GetParentGroup();
}
