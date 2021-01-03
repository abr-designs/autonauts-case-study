using System;
using TMPro;
using UnityEngine;

public class InteractCommandElement : CommandElementBase
{
    [SerializeField]
    private TMP_Text commandText;
    
    //private IInteractable _interactable;
    private string _targetName;
    private bool _useAlt;
    private InteractableCommand.TYPE _type;
    
    public void Init(string targetName, bool useAlt, InteractableCommand.TYPE type)
    {
        _targetName = targetName;
        _useAlt = useAlt;
        _type = type;

        switch (_type)
        {
            case InteractableCommand.TYPE.ITEM:
                commandText.text = $"Pick up {targetName}";
                break;
            case InteractableCommand.TYPE.BUILDING:
                commandText.text = _useAlt ? $"Add to {targetName}":$"Take from {targetName}";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

    }
    
    public override ICommand GenerateCommand()
    {
        var bot = UIManager.Instance.selectedBot;
        return new InteractableCommand(bot.transform, bot, _useAlt, bot, _targetName, _type);
    }
}
