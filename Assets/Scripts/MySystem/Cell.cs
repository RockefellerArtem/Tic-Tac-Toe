using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private Image _icon;

    private Image _itemIcon = default;
    private UIButton _button = default;

    private Player _busyPlayer = default;
    public Action<Cell> CallbackEvent { get; set; }

    private void ClickCell()
    {
        
        if (_busyPlayer != null) return;

        _icon.gameObject.SetActive(true);

        CallbackEvent?.Invoke(this);

    }

    public void SetPlayer(Player player)
    {
        _busyPlayer = player;
    }

    public void Init()
    {
        _button = GetComponent<UIButton>();
        _itemIcon = GetComponentInChildren<Image>();
        _button.Subscribe(ClickCell);
    }

    public void Subscribe(Action<Cell> callback)
    {
        CallbackEvent = callback;
    }
}