using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractCommandElement : CommandElementBase
{
    [SerializeField]
    private TMP_Text commandText;
    
    private IInteractable _interactable;
    
    public void Init(IInteractable interactable)
    {
        _interactable = interactable;

        switch (interactable)
        {
            case Item item:
                commandText.text = $"Pickup {item.ItemData.Name}";
                break;
            //TODO Need to understand context of removing or adding items
            case Building building:
                commandText.text = $"Take {building.heldItem.Value.Name} from {building.Name}";
                break;
        }
    }
    
    public override ICommand GenerateCommand()
    {
        var bot = UIManager.Instance.selectedBot;
        return new InteractableCommand(bot.transform, bot, bot);
    }
}
