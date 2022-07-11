using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Button[] _crossZero;

    [SerializeField] private List<Cell> _cells;

    private void Start()
    {
        foreach (var cell in _cells)
        {
            cell.Subscribe((callbackCell) =>
            {
                //����� ���������� ��������� ������� ������!
                //������ ����� ���� ������ ������� � ��������� �����!
            });
        }
    }



    public void TrackingProgress(int id)
    {
        _crossZero[id].enabled = false;
    }

    public void CheckLocate(bool isLocate)
    {
        foreach (var step in _crossZero)
        {
            if (isLocate == true)
            {
                //���� ������ ������, �� ���� ������ ������� � � ������� ����� �������� enable 
                step.interactable = false;
                Debug.Log("������ ���������");
            }
        }
    }
}
