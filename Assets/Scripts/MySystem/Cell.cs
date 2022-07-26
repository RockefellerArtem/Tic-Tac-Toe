using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private Image _icon;

    [Header("Sprites")]
    [SerializeField] private Sprite _cross;
    [SerializeField] private Sprite _zero;

    public TypeItem Type { get; private set; }

    private UIButton _button = default;

    private bool _isClick = true;

    public Action<Cell> CallbackEvent { get; set; }

    private void ClickCell()
    {
        if (_icon == true)
        {
            _button.enabled = false;
        }

        CallbackEvent?.Invoke(this);
    }

    public void SetSprite(TypeItem typeItem)
    {
        _icon.gameObject.SetActive(true);

        switch (typeItem)
        {
            case TypeItem.Cross:
                _icon.sprite = _cross;
                _isClick = false;
                break;

            case TypeItem.Zero:
                _icon.sprite = _zero;
                _isClick = false;
                break;

            case TypeItem.Empty:
                _isClick = true;
                _icon.sprite = null;
                _icon.gameObject.SetActive(false);
                break;

            default:
                break;
        }

        Type = typeItem;
    }

    public void Init()
    {
        Type = TypeItem.Empty;
        _button = GetComponent<UIButton>();
        _button.Subscribe(ClickCell);
    }

    public void Subscribe(Action<Cell> callback)
    {
        CallbackEvent = callback;
    }
}