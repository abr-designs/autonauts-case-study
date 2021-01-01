using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandElementFactory : MonoBehaviour
{
    //Instance
    //====================================================================================================================//
    
    public static CommandElementFactory Instance => _instance;
    private static CommandElementFactory _instance;

    private void Awake()
    {
        _instance = this;
    }

    //====================================================================================================================//
    
    [SerializeField]
    private LoopCommandElement loopCommandElement;
    [SerializeField]
    private SearchCommandElement searchCommandElement;
    [SerializeField]
    private MoveCommandElement moveCommandElement;
    [SerializeField]
    private InteractCommandElement interactCommandElement;

    //====================================================================================================================//

    public void GenerateCodeIn(in RectTransform rectTransform, IEnumerable<ICommand> commands)
    {
        foreach (var command in commands)
        {
            switch (command)
            {
                case LoopCommandBase loopCommandBase:
                    var loop = Instantiate(loopCommandElement, rectTransform);
                    GenerateCodeIn(loop.transform, loopCommandBase.InternalCommands);
                    break;
                case InteractableCommand interactableCommand:
                    var interactable = Instantiate(interactCommandElement, rectTransform);
                    break;
                
                case MoveCommand moveCommand:
                    var move = Instantiate(moveCommandElement, rectTransform);
                    break;
                case MoveToStoredTargetCommand moveCommand:
                    var moveToTarget = Instantiate(moveCommandElement, rectTransform);
                    break;
                case SearchCommand searchCommand:
                    var search = Instantiate(searchCommandElement, rectTransform);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(command), command, null);

            }
        }
    }
}
