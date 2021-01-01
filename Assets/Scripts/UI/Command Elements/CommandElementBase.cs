using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class CommandElementBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public new RectTransform transform { get; private set; }

    protected Image image;
    protected Color StartingColor;

    protected void Awake()
    {
        transform = gameObject.transform as RectTransform;

        image = GetComponent<Image>();
        StartingColor = image.color;
    }

    //====================================================================================================================//
    
    public abstract ICommand GenerateCommand();

    public CommandElementBase GetParentGroup()
    {
        return GetComponentInParent<CommandElementBase>();
    }


    //====================================================================================================================//
    
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        DarkenColor();
        GetParentGroup()?.DarkenColor();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        ResetColor();
        GetParentGroup()?.ResetColor();
    }
    
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) 
            return;
        
        Destroy(gameObject);
        UIManager.ForceUpdateLayouts();
    }

    //====================================================================================================================//

    public void SetColor(Color color)
    {
        image.color = color;
    }

    public void LightenColor()
    {
        Color.RGBToHSV(StartingColor, out var H, out var S, out var V);

        S = 0.6f;

        image.color = Color.HSVToRGB(H, S, V);
    }
    
    public void DarkenColor()
    {
        Color.RGBToHSV(StartingColor, out var H, out var S, out var V);

        V = 0.6f;

        image.color = Color.HSVToRGB(H, S, V);
    }

    public void ResetColor()
    {
        image.color = StartingColor;
    }

    //====================================================================================================================//
    
}
