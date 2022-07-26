using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Window")]
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _game;
    [SerializeField] private GameObject _result;

    [Header("ControllerResult")]
    [SerializeField] private TMP_Text _textResult;
    [SerializeField] private Image _iconResult;

    [Header("ScorePanel")]
    [SerializeField] private TMP_Text _scoreZero;
    [SerializeField] private TMP_Text _scoreCross;


    [Header("Sprites")]
    [SerializeField] private Sprite _cross;
    [SerializeField] private Sprite _zero;

    [SerializeField] private Text _roundText;


    public static GameManager Instance;
    private const int _countCells = 9;

    private int _countRound = 1;

    private int _scoreX = 0;
    private int _scoreO = 0;

    [SerializeField] private ButtonMode[] _modeButtons;

    private TypeMode _currentMode;

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
    }

    public void Next()
    {
        _result.SetActive(false);
        foreach (var cell in _cells)
        {
            Destroy(cell.gameObject);
        }
        _cells = new List<Cell>();
        StartGame();
    }

    public void Menu()
    {
        _game.SetActive(false);
        _result.SetActive(false);
        _menu.SetActive(true);

        foreach (var cell in _cells)
        {
            Destroy(cell.gameObject);
        }
        _cells = new List<Cell>();

        _scoreCross.text = $"{_scoreX = 0}";
        _scoreZero.text = $"{_scoreO = 0}";
        _roundText.text = $"Раунд {_countRound = 1}";
    }

    public void Select(ButtonMode sender)
    {
        foreach (var modeButton in _modeButtons)
        {
            modeButton.SetOutline(modeButton == sender);
        }

        if (sender == _modeButtons[0])
        {
            _currentMode = TypeMode.OneXFriend;
        }
        if(sender == _modeButtons[1])
        {
            _currentMode = TypeMode.OneXBot;
        }

        sender.SetGameMode(_currentMode);
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
                    _result.SetActive(true);
                    _textResult.text = "Победил";
                    _iconResult.sprite = _cross;
                    _scoreX++;
                    _scoreCross.text = $"{_scoreX}";
                    _iconResult.gameObject.SetActive(true);
                    break;

                case TypeItem.Zero:
                    _result.SetActive(true);
                    _textResult.text = "Победил";
                    _iconResult.sprite = _zero;
                    _scoreO++;
                    _scoreZero.text = $"{_scoreO}";
                    _iconResult.gameObject.SetActive(true);
                    break;
            }
            ReStart();
            return;
        }

        if (_cells.All((t)=> t.Type != TypeItem.Empty))
        {
            _result.SetActive(true);
            _iconResult.gameObject.SetActive(false);
            _textResult.text = " Ничья";
            ReStart();
        }

    }

    private void ReStart()
    {
        _roundText.text = $"Раунд {++_countRound}";
        for (int i = 0; i < _cells.Count; i++)
        {
            _cells[i].SetSprite(TypeItem.Empty);
        }
    }

    

    public void StartGame()
    {
        for (int i = 0; i < _countCells; i++)
        {
            var cell = FactoryCell.Instance.CreateCell();
            cell.Subscribe(HandlerCell);
            _cells.Add(cell);
        }
        
        SetFirstPlayer();
        _menu.SetActive(false);
        _game.SetActive(true);
    }
}
