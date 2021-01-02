using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DragController))]
public class UIManager : MonoBehaviour
{
    public static UIManager Instance => _instance;
    private static UIManager _instance;

    private void Awake()
    {
        _instance = this;
    }

    //====================================================================================================================//

    [SerializeField]
    private GameObject codeWindow;
    /*[SerializeField]
    private Button generateCommandsButton;*/
    [SerializeField]
    private TMP_InputField nameInputField;
    [SerializeField]
    private Button closeCodeWindowButton;
    [SerializeField]
    private RectTransform codeContainerTransform;
    //====================================================================================================================//

    public DragController DragController
    {
        get
        {
            if (_dragController == null)
                _dragController = GetComponent<DragController>();

            return _dragController;
        }
    }
    private DragController _dragController;

    private new Transform transform;

    //Unity Functions
    //====================================================================================================================//

    private void Start()
    {
        transform = gameObject.transform;

        InitButtons();
        SetCodeWindowActive(false, null);
    }

    //====================================================================================================================//

    public Bot selectedBot { get; private set; }

    public void SetCodeWindowActive(bool state, Bot bot)
    {
        codeWindow.SetActive(state);
        
        if (!state)
        {
            ClearCodeUI();
            selectedBot = null;
            return;
        }
        selectedBot = bot;


        nameInputField.text = bot.name;
        GenerateCodeUI();
    }

    private void InitButtons()
    {
        /*generateCommandsButton.onClick.AddListener(() =>
        {
            GenerateCodeUI();
            //var commands = GenerateCode();
        });*/
        
        nameInputField.onValueChanged.AddListener(SetBotName);
        
        
        closeCodeWindowButton.onClick.AddListener(() =>
        {
            SetCodeWindowActive(false, null);
        });
    }

    //====================================================================================================================//

    private void ClearCodeUI()
    {
        while (codeContainerTransform.childCount > 0)
        {
            var temp = codeContainerTransform.GetChild(0);
            Destroy(temp.gameObject);
        }
    }

    //====================================================================================================================//
    
    private void GenerateCodeUI()
    {
        CommandElementFactory.Instance.GenerateCodeIn(codeContainerTransform, selectedBot.Command);
    }

    private IEnumerable<ICommand> GenerateCode()
    {
        var childCount = codeContainerTransform.childCount;
        var commandElements = new List<CommandElementBase>();

        for (int i = 0; i < childCount; i++)
        {
            var ceb = codeContainerTransform.GetChild(i).GetComponent<CommandElementBase>();

            if (ceb is null)
                continue;
            
            commandElements.Add(ceb);
        }

        return commandElements.Count == 0 ? null : commandElements.Select(commandElementBase => commandElementBase.GenerateCommand());
    }

    //====================================================================================================================//

    private void SetBotName(string newName)
    {
        selectedBot.name = newName;
    }

    //====================================================================================================================//
    
    
    
    public static void ForceUpdateLayouts()
    {
        var layouts = Instance.GetComponentsInChildren<LayoutGroup>();

        foreach (var layoutGroup in layouts)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.transform as RectTransform);
        }
    }
}
