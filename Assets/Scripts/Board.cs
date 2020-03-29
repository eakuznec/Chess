using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(GridLayoutGroup))]
public class Board : MonoBehaviour
{
    private GridLayoutGroup _grid;
    [SerializeField] private int _size = 8;
    [HideInInspector] public Cell[,] cells = null;
    private List<Cell> _avaliableTarget = new List<Cell>();

    private void Awake()
    {
        CreateBoard();
    }

    public int size
    {
        get => _size;
    }

    private void CreateBoard()
    {
        cells = new Cell[_size, _size];
        _grid = GetComponent<GridLayoutGroup>();
        _grid.constraintCount = _size;
        RectTransform rect = GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _size * _grid.cellSize.x);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _size * _grid.cellSize.y);

        for (int i=0; i < _size; i++)
        {
            for(int j=0; j < _size; j++)
            {
                Cell cell = Instantiate(Resources.Load<Cell>("Cell"), _grid.transform);
                cell.pos = new Position(j, i);
                cells[j, i] = cell;
            }
        }
    }

    public Cell GetCell(int x, int y)
    {
        if (x < _size && y < _size && x>=0 && y>=0)
        {
            return cells[x, y];
        }

        return null;
    }


    public Cell GetCell(Position pos)
    {
        return GetCell(pos.x, pos.y);
    }

    public List<Cell> GetNeighbourCells(Cell cell)
    {
        List<Cell> retVal = new List<Cell>();
        if(cell.x - 1>= 0)
        {
            retVal.Add(GetCell(cell.x - 1, cell.y));
        }
        if (cell.x+1 < _size)
        {
            retVal.Add(GetCell(cell.x + 1, cell.y));
        }
        if (cell.y - 1 >= 0)
        {
            retVal.Add(GetCell(cell.x, cell.y - 1));
        }
        if (cell.y + 1 < _size)
        {
            retVal.Add(GetCell(cell.x, cell.y + 1));
        }
        return retVal;
    }

    public void ClearAvaliableTarget()
    {
        foreach (Cell cell in _avaliableTarget)
        {
            cell.hightlight = false;
        }
        _avaliableTarget.Clear();
    }

    public List<Cell> avaliableTarget
    {
        get => _avaliableTarget;
        set
        {
            ClearAvaliableTarget();
            foreach (Cell cell in value)
            {
                cell.hightlight = true;
                _avaliableTarget.Add(cell);
            }
        }
    }
}
