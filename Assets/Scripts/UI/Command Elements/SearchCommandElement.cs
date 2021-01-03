using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchCommandElement : CommandElementBase
{
    [SerializeField]
    private TMP_Text Text;

    [SerializeField]
    private Button targetButton;


    private ItemData _itemData;

    private Vector3 _location;

    private float _radius;
    
    //====================================================================================================================//

    private void Start()
    {
        targetButton.onClick.AddListener(() =>
        {
            //TODO Open the target selection
        });
    }

    //====================================================================================================================//

    public void Init(ItemData itemData, Vector3 location, float radius)
    {
        _itemData = itemData;
        _location = location;
        _radius = radius;

        Text.text = $"Find nearest {itemData.Name} in ";
    }
    
    
    //TODO Need to get this hooked up with some sort of manager
    public override ICommand GenerateCommand()
    {
        //var testBot = FindObjectOfType<TestBot>();
        var bot = UIManager.Instance.selectedBot;
        
        return new SearchCommand(bot.transform, bot, _itemData, _location, _radius);
    }

    
}
