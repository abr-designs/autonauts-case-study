using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveCommandElement : CommandElementBase
{
    [SerializeField]
    private TMP_Text Text;
    
    public override ICommand GenerateCommand()
    {
        var testBot = FindObjectOfType<TestBot>();
        return new MoveToStoredTargetCommand(testBot.transform, testBot, testBot.speed);
    }

}
