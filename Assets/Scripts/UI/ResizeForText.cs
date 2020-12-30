using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResizeForText : MonoBehaviour
{
    [SerializeField]
    private Vector2 offset;
    
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private RectTransform targetTransform;

    private string _text = string.Empty;

    private void Update()
    {
        if (_text.Equals(text.text))
            return;

        _text = text.text;
        ForceResize();
    }

    private void ForceResize()
    {
        int count = _text.Length;
        var width = (count * text.fontSize)/2f;

        var currentSize = targetTransform.sizeDelta;
        currentSize.x = width + offset.x;
        
        targetTransform.sizeDelta = currentSize;
        
        UIManager.ForceUpdateLayouts();
    }
    
    /*int GetTextSize(string message)
    {
        int totalLength = 0;
 
        var myFont = text.font;  //chatText is my Text component
        CharacterInfo characterInfo = new CharacterInfo();
 
        char[] arr = message.ToCharArray();
 
        foreach(char c in arr)
        {
            myFont.GetCharacterInfo(c, out characterInfo, text.fontSize);  
            myFont.
 
            totalLength += characterInfo.advance;
        }
 
        return totalLength;
    }*/
}
