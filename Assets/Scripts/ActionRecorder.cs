using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionRecorder : MonoBehaviour
{

    private Button _recordButton;
    private GameObject _fadeImageObject;

    public bool IsRecording { get; private set; }

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
    
}
