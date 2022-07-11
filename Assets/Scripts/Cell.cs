using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IEventer<Cell>
{

    private bool _isBusy;
    private Image _image;

    private UIButton _button;


    [SerializeField] private int _id;

    private bool _islocate;

    public Action<Cell> CallbackEvent { get; set; }

    private void Start()
    {
        Debug.Log($"ίχεικΰ {_id} {_islocate}");

        GetComponent<Button>().onClick.AddListener(() =>
        {
            _islocate = true;

            //GameManager.Instance.CheckLocate(_islocate);

            Debug.Log($"ίχεικΰ {_id} {_islocate}");
        });
    }

    public void Subscribe(Action<Cell> callback)
    {
        CallbackEvent = callback;
    }
}
