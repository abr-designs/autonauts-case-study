using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private Button generateCommandsButton;

    [SerializeField]
    private RectTransform codeContainerTransform;

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
    }

    //====================================================================================================================//

    private void InitButtons()
    {
        generateCommandsButton.onClick.AddListener(() =>
        {
            GenerateCodeUI();
            //var commands = GenerateCode();
        });
    }

    private void GenerateCodeUI()
    {
        var testBot = FindObjectOfType<TestBot>();
        
        CommandElementFactory.Instance.GenerateCodeIn(codeContainerTransform, testBot.Command);
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
    
    public static void ForceUpdateLayouts()
    {
        var layouts = Instance.GetComponentsInChildren<LayoutGroup>();

        foreach (var layoutGroup in layouts)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.transform as RectTransform);
        }
    }
}
