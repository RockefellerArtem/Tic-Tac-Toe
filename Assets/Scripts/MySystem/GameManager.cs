using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Load window")]
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _game;

    [SerializeField] private Sprite _O;
    [SerializeField] private Sprite _X;
    [SerializeField] private Sprite _draw;

    public static GameManager Instance;
    private const int _countCells = 9;

    [SerializeField] private ButtonMode[] _modeButtons;

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
        foreach (var modeButton in _modeButtons)
        {
            modeButton.SubcribeButtonMode(Select);
        }

        for (int i = 0; i < _countCells; i++)
        {
            var cell = FactoryCell.Instance.CreateCell();
            cell.Subscribe(HandlerCell);
            _cells.Add(cell);
        }
        StartGame();
    }

    public void Select(ButtonMode sender)
    {
        foreach (var modeButton in _modeButtons)
        {
            modeButton.SetOutline(modeButton == sender);
        }
    }

    public void Enable()
    {
        _menu.SetActive(false);
        _game.SetActive(true);
    }

    private void SetFirstPlayer()
    {
        var random = Random.Range(0, 9999);
        var randomIndexType = random % 2 == 0;

        if (randomIndexType)
        {
            _currentType = TypeItem.Cross;
            
        }
        else
        {
            _currentType = TypeItem.Zero;
        }

        var currentPlayer = _currentType;
        Debug.Log(currentPlayer);  
    }

    private void HandlerCell(Cell cell)
    {
        cell.SetSprite(_currentType);

        Debug.Log("CheckWin");
        CheckVariantWinToCells();

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
        }
        else
        {
            _currentType = TypeItem.Zero;
        }
    }

    private void CheckVariantWinToCells()
    {
        for (int i = 0; i < 8; i++)
        {
            bool isCheck = false;
            for (int j = 0; j < 3; j++)
            {
                Cell cell = _cells[_variants[i, j]];
                if (cell.Type != _currentType)
                {
                    isCheck = true;
                    break;
                }
            }
            if (isCheck) continue;

            switch (_currentType)
            {
                case TypeItem.Cross:
                    Debug.Log("победил X");
                    
                    break;

                case TypeItem.Zero:
                    Debug.Log("победил O");
                    break;

                case TypeItem.Empty:
                    Debug.Log("победил ");
                    break;

                default:
                    Debug.Log("ничь€");
                    break;
            }
            //Debug.Log("победил " + _currentType);
            ReStart();
            return;
        }

        if (_cells.All((t)=> t.Type != TypeItem.Empty))
        {
            Debug.Log("Ќичь€/");
            ReStart();
        }

    }

    private void ReStart()
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            _cells[i].SetSprite(TypeItem.Empty);
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
}
