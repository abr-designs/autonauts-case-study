using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoopCommandElement : CommandElementBase
{
    [SerializeField]
    private TMP_Dropdown typeDropdown;

    [SerializeField]
    private TMP_InputField numberInputField;
    
    [SerializeField]
    private Button targetToggle;
    
    [SerializeField]
    private Toggle escapeToggle;
    
    // Start is called before the first frame update
    private void Start()
    {
        typeDropdown.ClearOptions();
        typeDropdown.AddOptions(GetOptionsText(null));
        typeDropdown.onValueChanged.AddListener(OnDropdownChanged);

        OnDropdownChanged(0);
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

    public override CommandElementBase GetParentGroup()
    {
        throw new System.NotImplementedException();
    }


    //====================================================================================================================//

    private IEnumerable<ICommand> GetCommandInChildren()
    {
        var outList = new List<ICommand>();
        var childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            var child = transform.GetChild(i);

            var ceb = child.gameObject.GetComponent<CommandElementBase>();
            
            if(ceb == null)
                continue;
            
            outList.Add(ceb.GenerateCommand());
        }

        return outList;
    }
    
    //====================================================================================================================//
    

    private void OnTargetChanged()
    {
        
    }
    private void OnDropdownChanged(int value)
    {
        switch (value)
        {
            case 0:
                SetObjectsActive(false, false, true);
                break;
            case 1:
                SetObjectsActive(true, false, true);
                break;
            case 2:
            case 3:
            case 4:
            case 5:
                SetObjectsActive(false, false, true);
                break;
            case 6:
            case 7:
            case 8:
            case 9:
                SetObjectsActive(false, true, true);
                break;
            
        }
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        /*Canvas.ForceUpdateCanvases();
        var verticalLayoutGroup = FindObjectsOfType<VerticalLayoutGroup>();

        foreach (var layoutGroup in verticalLayoutGroup)
        {
            layoutGroup.enabled = false;
            layoutGroup.enabled = true;
        }*/

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
