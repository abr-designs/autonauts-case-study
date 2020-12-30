using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchCommandElement : CommandElementBase
{
    private TMP_Text Text;

    [SerializeField]
    private Button targetButton;

    //====================================================================================================================//

    private void Start()
    {
        targetButton.onClick.AddListener(() =>
        {
            //TODO Open the target selection
        });
    }

    //====================================================================================================================//
    
    
    //TODO Need to get this hooked up with some sort of manager
    public override ICommand GenerateCommand()
    {
        var testBot = FindObjectOfType<TestBot>();
        return new SearchCommand(testBot.transform, testBot, FindObjectOfType<ObjectManager>(), Vector3.zero, 20f);
    }

    
}
