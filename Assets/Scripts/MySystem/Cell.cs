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

    private UIButton _button = default;

    private Player _busyPlayer = default;
    public Action<Cell> CallbackEvent { get; set; }

    private void ClickCell()
    {
        if (_busyPlayer != null) return;

        _icon.gameObject.SetActive(true);

        CallbackEvent?.Invoke(this);
    }

    public void SetSprite(TypeItem typeItem)
    {
        if (typeItem == TypeItem.Cross)
        {
            _icon.sprite = _cross;
        }

        if (typeItem == TypeItem.Zero)
        {
            _icon.sprite = _zero;
        }


    }

    public void Init()
    {
        _button = GetComponent<UIButton>();
        _button.Subscribe(ClickCell);
    }

    public void Subscribe(Action<Cell> callback)
    {
        CallbackEvent = callback;
    }
}