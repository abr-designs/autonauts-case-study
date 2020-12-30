using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCommandElement : CommandElementBase
{
    public override ICommand GenerateCommand()
    {
        var testBot = FindObjectOfType<TestBot>();
        return new InteractableCommand(testBot.transform, testBot, testBot);
    }
}
