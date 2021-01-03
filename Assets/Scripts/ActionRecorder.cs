using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionRecorder : MonoBehaviour
{
    private Button _recordButton;
    private GameObject _fadeImageObject;

    //====================================================================================================================//
    
    public static bool CanSelectItems => IsRecording && !(SelectingTarget || SettingSearchArea);
    public static bool CanSelectBuildings => IsRecording && !SettingSearchArea;
    
    public static bool IsRecording { get; private set; }
    public static bool SelectingTarget { get; private set; }
    public static bool SettingSearchArea { get; private set; }

    //====================================================================================================================//
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //====================================================================================================================//

    public void InitButtons(Button recordButton, GameObject fadeImageObject)
    {
        _recordButton = recordButton;
        _fadeImageObject = fadeImageObject;
    }
    
    //====================================================================================================================//
    public void ToggleIsRecording()
    {
        SetIsRecording(!IsRecording);
    }
    public void SetIsRecording(bool recording)
    {
        _fadeImageObject.SetActive(!recording);
        _recordButton.image.color = recording ? Color.red : Color.white;
        IsRecording = recording;
    }

    public void SelectBuildingTarget(bool state)
    {
        //Set the UI To show we're picking a building
        
        SelectingTarget = state;
    }
    public void SetSearchTarget(bool state)
    {
        //Set the UI To show we're picking a building
        
        SettingSearchArea = state;
    }
    
    //====================================================================================================================//

    public static void RecordActions(IEnumerable<ICommand> actions)
    {
        CommandElementFactory.Instance.GenerateCodeIn(UIManager.Instance.CodeContainerTransform, actions);
    }

    public static void SetBuildingTarget(Building building)
    {
        
    }
    public static void SetSearchTarget(Vector3 searchTarget)
    {
        
    }

    //====================================================================================================================//
    
}
