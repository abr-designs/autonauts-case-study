using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class LoopCommandElement : CommandElementBase
{
    public enum TYPE
    {
        INFINITE,
        COUNT,
        CONDITIONAL
    }
    [SerializeField]
    private TMP_Dropdown typeDropdown;

    [SerializeField]
    private TMP_InputField numberInputField;
    
    [SerializeField]
    private Button targetToggle;
    
    [SerializeField]
    private Toggle escapeToggle;

    private GameObject _target;
    
    //Setup of Loop
    //====================================================================================================================//

    public void Init(LoopCommandBase loopCommandBase)
    {
        typeDropdown.ClearOptions();
        typeDropdown.AddOptions(GetOptionsText(null));
        typeDropdown.onValueChanged.AddListener(OnDropdownChanged);
        
        switch (loopCommandBase)
        {
            case InfiniteLoopCommand _ :
                SetLoopType(TYPE.INFINITE);
                break;
            case FixedLoopCommand fixedLoopCommand:
                SetLoopType(TYPE.COUNT);
                numberInputField.text = fixedLoopCommand.Count.ToString();
                break;
            case ConditionalLoopCommand conditionalLoopCommand:
                SetLoopType(TYPE.CONDITIONAL, conditionalLoopCommand.Condition);
                break;
        }
    }

    private void SetTarget(GameObject target)
    {
        _target = target;
        OnTargetChanged();
    }
    
    private void SetLoopType(TYPE loopType, CONDITION conditional = CONDITION.HANDS_FULL)
    {
        switch (loopType)
        {
            case TYPE.INFINITE:
            case TYPE.COUNT:
                typeDropdown.value = (int)loopType;
                break;
            case TYPE.CONDITIONAL:
                typeDropdown.value = (int)loopType + (int)conditional;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(loopType), loopType, null);
        }
        typeDropdown.Select();
        typeDropdown.RefreshShownValue();
        OnDropdownChanged(typeDropdown.value);

    }

    //====================================================================================================================//
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) 
            return;

        var children = GetCommandElementsInChildren().ToArray();

        if (children.Length > 0)
        {
            var parent = transform.parent;
            var siblingIndex = transform.GetSiblingIndex();
            
            for (int i = children.Length - 1; i >= 0; i--)
            {
                var child = children[i].transform;
                
                child.SetParent(parent);
                child.SetSiblingIndex(siblingIndex);
            }
        }

        
        Destroy(gameObject);
        UIManager.ForceUpdateLayouts();
    }

    //====================================================================================================================//

    public override ICommand GenerateCommand()
    {
        var internalCommands = GetCommandInChildren();

        switch (typeDropdown.value)
        {
            //Forever
            case  0:
                return new InfiniteLoopCommand(internalCommands);
            //Count
            case  1:
                return new FixedLoopCommand(internalCommands, int.Parse(numberInputField.text));
            //Conditional
            default:
                return new ConditionalLoopCommand(internalCommands, 
                    FindObjectOfType<TestBot>(),
                    (CONDITION)typeDropdown.value - 2);
        }
    }

    //====================================================================================================================//

    private IEnumerable<ICommand> GetCommandInChildren()
    {
        var children = GetCommandElementsInChildren().ToArray();

        return children.Any()
            ? children.Select(commandElementBase => commandElementBase.GenerateCommand())
            : null;
    }
    
    private IEnumerable<CommandElementBase> GetCommandElementsInChildren()
    {
        var outList = new List<CommandElementBase>();
        var childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            var child = transform.GetChild(i);

            var ceb = child.gameObject.GetComponent<CommandElementBase>();
            
            if(ceb == null)
                continue;
            
            outList.Add(ceb);
        }

        return outList;
    }
    
    //====================================================================================================================//
    


    //====================================================================================================================//
    

    private void OnTargetChanged()
    {
        var current = typeDropdown.value;
        
        typeDropdown.ClearOptions();
        typeDropdown.AddOptions(GetOptionsText(_target));

        typeDropdown.value = current;
        typeDropdown.onValueChanged?.Invoke(current);
    }
    private void OnDropdownChanged(int value)
    {
        switch (value)
        {
            case 0://Forever
                SetObjectsActive(false, false, true);
                break;
            case 1://Count
                SetObjectsActive(true, false, true);
                break;
            case 2://Conditional Hands
            case 3:
            case 4:
            case 5:
                SetObjectsActive(false, false, true);
                break;
            case 6://Conditional Buildings
            case 7:
            case 8:
            case 9:
                SetObjectsActive(false, true, true);
                break;
            
        }
        
        UIManager.ForceUpdateLayouts();
    }

    private void SetObjectsActive(bool inputActive, bool targetActive, bool escActive)
    {
        numberInputField.gameObject.SetActive(inputActive);
        targetToggle.gameObject.SetActive(targetActive);
        escapeToggle.gameObject.SetActive(escActive);
    }
    
    //====================================================================================================================//
    
    private static List<string> GetOptionsText(Object target)
    {
        var insert = target == null ? "?" : target.name;
        
        return new List<string>
        {
            "forever!",
            "times",
            "until hands full",
            "until hands not full",
            "until hands empty",
            "until hands not empty",
            $"until {insert} full",
            $"until {insert} not full",
            $"until {insert} empty",
            $"until {insert} full",
        };
    }
}
