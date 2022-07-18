using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentPlayerText;

    public static GameManager Instance;
    private const int _countCells = 9;

    private TypeItem _currentType;

    private TypeItem[] _typeItems = new TypeItem[2]
    {
        TypeItem.Zero,
        TypeItem.Cross
    };

    private List<Cell> _cells = new List<Cell>();

    private int[,] _variants = new int[8, 3]
    {
        {0,1,2 },
        {3,4,5 },
        {6,7,8 },
        {0,3,6 },
        {1,4,7 },
        {2,5,8 },
        {0,4,8 },
        {2,4,6 },
    };

    private void Start()
    {
        for (int i = 0; i < _countCells; i++)
        {
            var cell = FactoryCell.Instance.CreateCell();
            cell.Subscribe(HandlerCell);
            _cells.Add(cell);
        }
        StartGame();
    }

    private void SetFirstPlayer()
    {
        var random = Random.Range(0, 9999);
        var randomIndexType = random % 2 == 0;

        if (randomIndexType)
        {
            _currentType = TypeItem.Cross;
            _currentPlayerText.text = "Ходит Крестик";
        }
        else
        {
            _currentType = TypeItem.Zero;
            _currentPlayerText.text = "Ходит Нолик";
        }
    }

    private void HandlerCell(Cell cell)
    {
        cell.SetSprite(_currentType);

        NextPlayer();
    }

    private void NextPlayer()
    {
        var currentIndexPlayer = (int)_currentType;
        foreach (var typeItem in _typeItems)
        {
            if ((int)typeItem != currentIndexPlayer)
            {
                _currentType = typeItem;
            }
        }

        if ((int)_currentType == 0)
        {
            _currentType = TypeItem.Cross;
            _currentPlayerText.text = "Ходит Крестик";
        }
        else
        {
            _currentType = TypeItem.Zero;
            _currentPlayerText.text = "Ходит Нолик";
        }
    }

    private void CheckVariantWinToCells()
    {
        for (int i = 0; i < 8; i++)
        {
            var tempCells = new List<Cell>();
            for (int j = 0; j < 3; j++)
            {
                tempCells.Add(_cells[_variants[i, j]]);
            }

            foreach (var tempCell in tempCells)
            {
                //if ()
                //{
                //    Debug.Log("ВЫ ВЫИГРАЛИ");
                //}
                //else
                //{
                //    Debug.Log("ВЫ ПРОИГРАЛИ");
                //}

                //после победы искать нового первого игрока
            }
        }
    }

    // void SetGameMode(TypeMode type)

    //enum TypeMode {OneXFriend, OneXBot}

    private void StartGame()
    {
        SetFirstPlayer();
    }

    private void EndGame()
    {
        //перед
    }


    //public void CheckLocate(bool isLocate)
    //{
    //    foreach (var step in _crossZero)
    //    {
    //        if (isLocate == true)
    //        {
    //            //если ячейка занята, то туда нельзя сходить и у индекса будет выключен enable 
    //            step.interactable = false;
    //            Debug.Log("Нельзя поставить");
    //        }
    //    }
    //}
}
