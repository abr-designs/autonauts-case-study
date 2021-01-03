using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropCommandElement : CommandElementBase
{
    public override ICommand GenerateCommand()
    {
        var bot = UIManager.Instance.selectedBot;
        return new DropCommand(bot);
    }
}
