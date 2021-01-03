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
    
    public RectTransform CodeContainerTransform => codeContainerTransform;


    [SerializeField]
    private GameObject codeWindow;
    [SerializeField]
    private RectTransform arrowRectTransform;
    /*[SerializeField]
    private Button generateCommandsButton;*/
    [SerializeField]
    private TMP_InputField nameInputField;
    [SerializeField]
    private Button closeCodeWindowButton;
    [SerializeField]
    private RectTransform codeContainerTransform;

    [SerializeField] private GameObject fadeImageObject;
    //====================================================================================================================//

    [SerializeField, Space(10f)]
    private Button recordButton;
    [SerializeField]
    private Button playButton;

    private TMP_Text _playButtonText;
    [SerializeField]
    private Button stopButton;
    [SerializeField]
    private Button repeatButton;
    
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
    
    public ActionRecorder ActionRecorder
    {
        get
        {
            if (_actionRecorder == null)
                _actionRecorder = GetComponent<ActionRecorder>();

            return _actionRecorder;
        }
    }
    private ActionRecorder _actionRecorder;

    private new Transform transform;
    
    public Bot selectedBot { get; private set; }
    
    private CommandElementBase[] allCommandElements;


    //Unity Functions
    //====================================================================================================================//

    private void Start()
    {
        transform = gameObject.transform;

        arrowRectTransform.gameObject.SetActive(false);
        
        InitButtons();
        ActionRecorder.InitButtons(recordButton, fadeImageObject);
        SetCodeWindowActive(false, null);
    }

    //====================================================================================================================//

    private void UpdateButtons(bool botIsPaused)
    {
        recordButton.interactable = botIsPaused;
        playButton.interactable = botIsPaused;
        stopButton.interactable = !botIsPaused;
    }
    
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

        UpdateButtons(selectedBot.IsPaused);
        repeatButton.interactable = false;
        
        allCommandElements = codeContainerTransform.GetComponentsInChildren<CommandElementBase>();

    }

    private int _currentID = -1;
    public void HighlightCommandElement(int id)
    {
        if(allCommandElements == null || allCommandElements.Length == 0)
            return;

        if (id == _currentID)
            return;
        
        _currentID = id;

        var element = allCommandElements.FirstOrDefault(x => x.ID == id);
        arrowRectTransform.position = element.transform.position + Vector3.down * 30f;
    }

    private void InitButtons()
    {
        recordButton.onClick.AddListener(()=>
        {
            ActionRecorder.ToggleIsRecording();
            playButton.interactable = !ActionRecorder.IsRecording;
            repeatButton.interactable = ActionRecorder.IsRecording;

            if (!ActionRecorder.IsRecording)
            {
                var newCommands = GenerateCode();
                selectedBot.SetCommands(newCommands);
            }
            
            arrowRectTransform.gameObject.SetActive(false);
        });
        _playButtonText = playButton.GetComponentInChildren<TMP_Text>();
        playButton.onClick.AddListener(() =>
        {
            selectedBot.SetPaused(false);
            UpdateButtons(selectedBot.IsPaused);
            repeatButton.interactable = false;
            
            arrowRectTransform.gameObject.SetActive(true);
        });
        stopButton.onClick.AddListener(() =>
        {
            selectedBot.SetPaused(true);
            UpdateButtons(selectedBot.IsPaused);
            repeatButton.interactable = true;
            
            arrowRectTransform.gameObject.SetActive(false);
        });
        
        
        nameInputField.onValueChanged.AddListener(SetBotName);
        
        
        closeCodeWindowButton.onClick.AddListener(() =>
        {
            SetCodeWindowActive(false, null);
        });
        
        repeatButton.onClick.AddListener(WrapWithRepeat);
    }

    //====================================================================================================================//

    private void WrapWithRepeat()
    {
        var commands = new List<ICommand>();
        for (var i = 0; i < codeContainerTransform.childCount; i++)
        {
            var child = codeContainerTransform.GetChild(i).GetComponent<CommandElementBase>();
            
            if(child is null)
                continue;

            commands.Add(child.GenerateCommand());
            Destroy(child.gameObject);
        }

        CommandElementFactory.Instance.GenerateCodeIn(codeContainerTransform, 
            new ICommand[]
        {
            new InfiniteLoopCommand(commands)
        });
    }
    
    //====================================================================================================================//
    

    private void ClearCodeUI()
    {
        if (allCommandElements == null || allCommandElements.Length == 0)
            return;
        
        for (int i = allCommandElements.Length - 1; i >= 0; i--)
        {
            Destroy(allCommandElements[i].gameObject);
        }

        allCommandElements = new CommandElementBase[0];
    }

    //====================================================================================================================//
    
    private void GenerateCodeUI()
    {
        CommandElementFactory.Instance.GenerateCodeIn(codeContainerTransform, selectedBot.Command);
    }

    
    private IEnumerable<ICommand> GenerateCode()
    {
        allCommandElements = codeContainerTransform.GetComponentsInChildren<CommandElementBase>();
        for (int i = 0; i < allCommandElements.Length; i++)
        {
            allCommandElements[i].ID = i;
        }
        
        var childCount = codeContainerTransform.childCount;
        var commandElements = new List<CommandElementBase>();

        for (int i = 0; i < childCount; i++)
        {
            var ceb = codeContainerTransform.GetChild(i).GetComponent<CommandElementBase>();

            if (ceb is null)
                continue;
            
            commandElements.Add(ceb);
        }

        if (commandElements.Count == 0)
            return null;

        return commandElements.Select(commandElementBase => commandElementBase.GenerateCommand());
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

    //====================================================================================================================//
    
}
