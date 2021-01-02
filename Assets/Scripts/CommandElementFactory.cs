using System;
using System.Collections.Generic;
using System.Linq;
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
        if (commands == null || !commands.Any())
            return;
        
        foreach (var command in commands)
        {
            switch (command)
            {
                case LoopCommandBase loopCommandBase:
                    var loop = Instantiate(loopCommandElement, rectTransform);
                    loop.transform.pivot = new Vector2(0, 1);
                    loop.Init(loopCommandBase);
                    
                    GenerateCodeIn(loop.transform, loopCommandBase.InternalCommands);
                    break;
                case InteractableCommand interactableCommand:
                    var interactable = Instantiate(interactCommandElement, rectTransform);
                    interactable.transform.pivot = new Vector2(0, 1);
                    break;
                
                case MoveCommand moveCommand:
                    var move = Instantiate(moveCommandElement, rectTransform);
                    move.transform.pivot = new Vector2(0, 1);
                    break;
                case MoveToStoredTargetCommand moveCommand:
                    var moveToTarget = Instantiate(moveCommandElement, rectTransform);
                    moveToTarget.transform.pivot = new Vector2(0, 1);
                    break;
                case SearchCommand searchCommand:
                    var search = Instantiate(searchCommandElement, rectTransform);
                    search.transform.pivot = new Vector2(0, 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(command), command, null);

            }
        }
    }
}
