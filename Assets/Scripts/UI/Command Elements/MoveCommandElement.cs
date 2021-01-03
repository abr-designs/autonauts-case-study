using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveCommandElement : CommandElementBase
{
    [SerializeField]
    private TMP_Text Text;

    private Vector3 storedPosition;

    private string _targetName;
    private IInteractable _targetInteractable;
    
    public void Init(in string targetName)
    {
        _targetName = targetName;
        Text.text = $"Move to {_targetName}";
    }
    public void Init(in IInteractable targetInteractable)
    {
        if (!(targetInteractable is Building building))
            throw new Exception();
        
        Text.text = $"Move to {building.Name}";
        _targetInteractable = building;
    }
    public void Init(Vector3 position)
    {
        storedPosition = position;
        Text.text = $"Move to {Mathf.RoundToInt(storedPosition.x)}, {Mathf.RoundToInt(storedPosition.z)}";
    }
    
    
    public override ICommand GenerateCommand()
    {
        var bot = UIManager.Instance.selectedBot;

        if (_targetInteractable != null)
        {
            return new StoreAndMoveToStoredTargetCommand(bot.transform, bot, _targetInteractable, bot.Speed)
            {
                ID = ID
            };
        }

        if (!string.IsNullOrEmpty(_targetName))
        {
            return new MoveToStoredTargetCommand(bot.transform, bot, bot.Speed, _targetName)
            {
                ID = ID
            };
        }

        if (storedPosition != Vector3.zero)
        {
            return new MoveToPositionCommand(bot.transform, storedPosition, bot.Speed)
            {
                ID = ID
            };
        }

        throw new ArgumentOutOfRangeException();
    }

}
