using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private const int _countCells = 9;

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
            FactoryCell.Instance.CreateCell();
        }
    }

    private void NextPlayer()
    {

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
            }
        }
    }

    // void SetGameMode(TypeMode type)

    //enum TypeMode {OneXFriend, OneXBot}

    //void EndGame();

    //void StartGame();

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
