using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractCommandElement : CommandElementBase
{
    [SerializeField]
    private TMP_Text commandText;
    
    private IInteractable _interactable;
    private bool _useAlt;
    
    public void Init(IInteractable interactable, bool useAlt)
    {
        _interactable = interactable;
        _useAlt = useAlt;

        switch (interactable)
        {
            case Item item:
                commandText.text = $"Pickup {item.ItemData.Name}";
                break;
            //TODO Need to understand context of removing or adding items
            case Building building:
                commandText.text = _useAlt ? $"Add to {building.Name}" : $"Take from {building.Name}";
                break;
        }
    }
    
    public override ICommand GenerateCommand()
    {
        var bot = UIManager.Instance.selectedBot;
        return new InteractableCommand(bot.transform, bot, _useAlt, bot);
    }
}
