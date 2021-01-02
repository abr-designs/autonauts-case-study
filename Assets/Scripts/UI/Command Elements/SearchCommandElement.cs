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

    
    private Item _searchItem;

    //====================================================================================================================//

    private void Start()
    {
        targetButton.onClick.AddListener(() =>
        {
            //TODO Open the target selection
        });
    }

    //====================================================================================================================//

    public void Init(Item searchTarget)
    {
        _searchItem = searchTarget;
    }
    
    
    //TODO Need to get this hooked up with some sort of manager
    public override ICommand GenerateCommand()
    {
        //var testBot = FindObjectOfType<TestBot>();
        var bot = UIManager.Instance.selectedBot;
        
        return new SearchCommand(bot.transform, bot, _searchItem.ItemData, _searchItem.transform.position, 20f);
    }

    
}
