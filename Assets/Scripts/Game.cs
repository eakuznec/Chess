using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static Game _instance;
    [HideInInspector] public GameState state = GameState.GAME;
    [SerializeField] private GameSettings _gameSettings;
    public Board board;
    public List<Figure> figures = new List<Figure>();
    [HideInInspector] public bool whiteTurn;
    private Figure _dragFigure;
    [HideInInspector] public Cell mouseoverCell;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                DestroyImmediate(gameObject);
                return;
            }
        }
    }

    private void Start()
    {
        Reset();
    }

    public static Game instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Game>();
                if (_instance == null)
                {
                    _instance = Instantiate(Resources.Load<Game>("Game"));
                }
            }

            return _instance;
        }
    }

    public Figure dragFigure
    {
        get => _dragFigure;
        set
        {
            _dragFigure = value;
            if (_dragFigure)
            {
                board.avaliableTarget = _dragFigure.GetAvaliableTargets();
            }
            else
            {
                board.ClearAvaliableTarget();
            }
        }
    }

    public Figure GetFigureOnPosition(Position pos)
    {
        return board.GetCell(pos).figure;
    }

    public Figure GetFigureOnPosition(int x, int y)
    {
        return board.GetCell(x, y).figure;
    }

    public Figure CreateFigure(Figure figure, bool white, Position pos)
    {
        Figure retVal = null;
        bool canCreate = true;
        foreach (Figure fig in figures)
        {
            if (fig.cell.pos == pos)
            {
                canCreate = false;
                break;
            }
        }
        if (canCreate)
        {
            retVal = Instantiate(figure);
            retVal.white = white;
            retVal.SetPosition(pos);
            figures.Add(retVal);
        }
        else
        {
            Debug.LogError(string.Format("Не можем создать {0}. На клетке {1} уже есть фигура.", figure, pos));
        }

        return retVal;
    }

    public Figure CreateFigure(Figure figure, bool white, Cell cell)
    {
        return CreateFigure(figure, white, cell.pos);
    }

    public Figure CreateFigure(FigureStartSettings figureSetting)
    {
        return CreateFigure(figureSetting.figure, figureSetting.white, figureSetting.pos);
    }

    public void StartTurn(bool white)
    {
        whiteTurn = white;
        InterfaceManager.instance.StartTurn(whiteTurn);
    }

    public void Reset()
    {
        state = GameState.GAME;
        for(int i=0; i<figures.Count; i++)
        {
            figures[i].Kill();
            i--;
        }
        InterfaceManager.instance.Reset();
        if (_gameSettings != null)
        {
            foreach(FigureStartSettings figureSetting in _gameSettings.figureSettings)
            {
                CreateFigure(figureSetting);
            }
        }
        StartTurn(true);
    }

    public void EndGame(bool white)
    {
        InterfaceManager.instance.winPanel.Show(white);
    }
}

public enum GameState
{
    GAME,
    PROMOTION,
    WIN
}