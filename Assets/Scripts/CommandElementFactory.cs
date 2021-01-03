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
    
    [SerializeField]
    private DropCommandElement dropCommandElement;

    //====================================================================================================================//

    public void GenerateCodeIn(in RectTransform rectTransform, IEnumerable<ICommand> commands)
    {
        if (commands == null || !commands.Any())
            return;
        
        foreach (var command in commands)
        {
            GenerateCommandElement(rectTransform, command);
            /*switch (command)
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

            }*/
        }
    }
    
    public CommandElementBase GenerateCommandElement(in RectTransform rectTransform, ICommand command)
    {
        if (command == null)
            return null;
        
        switch (command)
        {
            //--------------------------------------------------------------------------------------------------------//
            case LoopCommandBase loopCommandBase:
                var loop = Instantiate(loopCommandElement, rectTransform);
                loop.transform.pivot = new Vector2(0, 1);
                loop.Init(loopCommandBase);
                loop.ID = loopCommandBase.ID;
                    
                GenerateCodeIn(loop.transform, loopCommandBase.InternalCommands);
                return loop;
            //--------------------------------------------------------------------------------------------------------//
            case InteractableCommand interactableCommand:
                var interactable = Instantiate(interactCommandElement, rectTransform);
                interactable.transform.pivot = new Vector2(0, 1);
                var data = new
                {
                    interactableCommand.Type,
                    interactableCommand.TargetName,
                    interactableCommand.UseAltInteraction
                };
                interactable.Init(data.TargetName,data.UseAltInteraction, data.Type);
                interactable.ID = interactableCommand.ID;
                
                return interactable;
            //--------------------------------------------------------------------------------------------------------//
            //case MoveCommand moveCommand:
            //    var move = Instantiate(moveCommandElement, rectTransform);
            //    move.transform.pivot = new Vector2(0, 1);
            //    return move;
            case MoveToPositionCommand moveToPosition:
                var moveToPos = Instantiate(moveCommandElement, rectTransform);
                moveToPos.transform.pivot = new Vector2(0, 1);
                moveToPos.Init(moveToPosition.Target);
                moveToPos.ID = moveToPosition.ID;
                
                return moveToPos;
            case StoreAndMoveToStoredTargetCommand storeAndMoveToStoredTargetCommand:
                var StoreMoveToTarget = Instantiate(moveCommandElement, rectTransform);
                StoreMoveToTarget.transform.pivot = new Vector2(0, 1);
                StoreMoveToTarget.Init(storeAndMoveToStoredTargetCommand.TargetInteractable);
                StoreMoveToTarget.ID = storeAndMoveToStoredTargetCommand.ID;
                
                return StoreMoveToTarget;
            case MoveToStoredTargetCommand moveCommand:
                var moveToTarget = Instantiate(moveCommandElement, rectTransform);
                moveToTarget.transform.pivot = new Vector2(0, 1);
                moveToTarget.Init(moveCommand.TargetName);
                moveToTarget.ID = moveCommand.ID;
                return moveToTarget;
            //--------------------------------------------------------------------------------------------------------//
            case SearchCommand searchCommand:
                var search = Instantiate(searchCommandElement, rectTransform);
                search.transform.pivot = new Vector2(0, 1);
                search.Init(searchCommand.ItemData, searchCommand.SearchLocation, searchCommand.Radius);
                search.ID = searchCommand.ID;
                return search;
            //--------------------------------------------------------------------------------------------------------//
            case DropCommand dropCommand:
                var drop = Instantiate(dropCommandElement, rectTransform);
                drop.transform.pivot = new Vector2(0, 1);
                drop.ID = dropCommand.ID;
                return drop;
            //--------------------------------------------------------------------------------------------------------//
            default:
                throw new ArgumentOutOfRangeException(nameof(command), command, null);

        }
    }
}
