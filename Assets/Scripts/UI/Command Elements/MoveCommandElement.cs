using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveCommandElement : CommandElementBase
{
    [SerializeField]
    private TMP_Text Text;

    private IInteractable _interactable;
    
    private Vector3 storedPosition;
    
    public void Init(IInteractable interactable)
    {
        _interactable = interactable;

        switch (interactable)
        {
            case Item item:
                Text.text = $"Move to {item.ItemData.Name}";
                break;
            case Building building:
                Text.text = $"Move to {building.Name}";
                break;
        }
    }
    public void Init(Vector3 position)
    {
        storedPosition = position;
        Text.text = $"Move to {Mathf.RoundToInt(storedPosition.x)}, {Mathf.RoundToInt(storedPosition.z)}";
    }
    
    
    public override ICommand GenerateCommand()
    {
        var bot = UIManager.Instance.selectedBot;

        switch (_interactable)
        {
            case Item _:
                return new MoveToStoredTargetCommand(bot.transform, bot, bot.Speed);
            case Building building:
                return new MoveCommand(bot.transform, building.transform, bot.Speed);
            default:
            {
                if (storedPosition != Vector3.zero)
                {
                    return new MoveToPositionCommand(bot.transform, storedPosition, bot.Speed);
                }

                break;
            }
        }

        throw new ArgumentOutOfRangeException();
    }

}
