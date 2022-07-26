using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMode : UIButton
{
    [SerializeField] private GameObject _outlineObject;

    private Action<ButtonMode> _callback;

    private void Start()
    {
        Subscribe(() => 
        { 
            _callback?.Invoke(this);
        });
    }


    public void SetGameMode(TypeMode typeMode)
    {
        switch (typeMode)
        {
            case TypeMode.OneXFriend:
                Debug.Log("friend");
                break;
            case TypeMode.OneXBot:
                Debug.Log("bot");
                break;
        }
    }

    public void SetOutline(bool isActive)
    {
        _outlineObject.SetActive(isActive);
    }

    public void SubcribeButtonMode(Action<ButtonMode> callback)
    {
        _callback += callback;
    }
}
