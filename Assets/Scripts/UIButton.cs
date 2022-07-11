using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool _isPointerDown = false;
    private bool _isPointerEnter = false;
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        _isPointerDown = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerEnter = false;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        _isPointerDown = false;
        Click();
    }

    private void Click()
    {
        if (_isPointerEnter == true)
        {
            Debug.Log("Click");
        }
    }
}
