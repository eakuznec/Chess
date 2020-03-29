using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent (typeof(Image))]
[RequireComponent (typeof(Animator))]
public abstract class Figure : MonoBehaviour
{
    private bool _white;
    private Cell _cell;
    [SerializeField] private Sprite whiteSprite;
    [SerializeField] private Sprite blackSprite;
    [HideInInspector] public bool firstMove = true;

    public bool white
    {
        get => _white;
        set
        {
            _white = value;
            if (_white)
            {
                GetComponent<Image>().sprite = whiteSprite;
            }
            else
            {
                GetComponent<Image>().sprite = blackSprite;
            }
        }
    }

    public Cell cell
    {
        get => _cell;
        set
        {
            if(_cell != null && _cell.figure == this)
            {
                _cell.figure = null;
            }
            _cell = value;
            _cell.figure = this;
        }
    }

    public int x
    {
        get => cell.x;
    }

    public int y
    {
        get => cell.y;
    }

    public void SetPosition(Cell cell, bool kill = false)
    {
        this.cell = cell;
        transform.SetParent(cell.transform);
        transform.SetPositionAndRotation(cell.transform.position, Quaternion.identity);
        if (kill)
        {
            transform.SetSiblingIndex(transform.GetSiblingIndex() - 1);
        }
    }

    public void SetPosition(Position pos)
    {
        SetPosition(Game.instance.board.GetCell(pos));
    }

    public void SetPosition(int x, int y)
    {
        SetPosition(Game.instance.board.GetCell(x, y));
    }

    public virtual void MoveOnCell(Cell cell)
    {
        KillOrMove(cell);
        EndTurn();
    }

    public virtual void Kill()
    {
        Game.instance.figures.Remove(this);
        GetComponent<Animator>().SetTrigger("Death");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public abstract List<Cell> GetAvaliableTargets();
       
    private int maxDistanceToBorder
    {
        get
        {
            int boardSize = Game.instance.board.size;
            return Mathf.Max(x, y, boardSize - 1 - x, boardSize - 1 - y);
        }
    }


    private void AddCellToList(List<Cell> list, ref bool flag, int xPos, int yPos)
    {
        if (!flag)
        {
            Cell targetCell = Game.instance.board.GetCell(xPos, yPos);
            if (!targetCell)
            {
                flag = true;
            }
            else
            {
                Figure targetFigure = targetCell.figure;
                if (!targetFigure)
                {
                    list.Add(targetCell);
                }
                else
                {
                    flag = true;
                    if (targetFigure.white != white)
                    {
                        list.Add(targetCell);
                    }
                }
            }
        }
    }

    public List<Cell> GetDiagonalCells()
    {
        List<Cell> retVal = new List<Cell>();
        bool bottomLeft = false;
        bool topLeft = false;
        bool bottomRight = false;
        bool topRight = false;
        for (int i = 1; i < maxDistanceToBorder; i++)
        {
            AddCellToList(retVal, ref bottomLeft, cell.x-i, cell.y - i);
            AddCellToList(retVal, ref topLeft, cell.x-i, cell.y + i);
            AddCellToList(retVal, ref bottomRight, cell.x + i, cell.y-i);
            AddCellToList(retVal, ref topRight, cell.x + i, cell.y+i);
        }

        return retVal;
    }

    public List<Cell> GetLinearCells()
    {
        List<Cell> retVal = new List<Cell>();
        bool bottom = false;
        bool top = false;
        bool right = false;
        bool left = false;
        for(int i=1; i<maxDistanceToBorder; i++)
        {
            AddCellToList(retVal, ref bottom, cell.x, cell.y - i);
            AddCellToList(retVal, ref top, cell.x, cell.y + i);
            AddCellToList(retVal, ref left, cell.x-i, cell.y);
            AddCellToList(retVal, ref right, cell.x + i, cell.y);
        }

        return retVal;
    }

    protected void KillOrMove(Cell cell)
    {
        bool kill = false;
        if (cell.figure)
        {
            kill = true;
            cell.figure.Kill();
        }
        SetPosition(cell, kill);
    }

    public void EndTurn()
    {
        firstMove = false;
        Game.instance.StartTurn(!white);
    }

    protected bool CheckCellOnMove(Cell targetCell)
    {
        if (targetCell && !targetCell.figure)
        {
            return true;
        }
        return false;
    }

    protected bool CheckCellOnMoveAndEat(Cell targetCell)
    {
        if (CheckCellOnMove(targetCell))
        {
            return true;
        }
        else if (CheckCellOnEat(targetCell))
        {
            return true;
        }
        return false;
    }

    protected bool CheckCellOnEat(Cell targetCell)
    {
        if(targetCell && targetCell.figure && targetCell.figure.white != white)
        {
            return true;
        }
        return false;
    }

    protected void AddCellToListOnMove(List<Cell> list, Cell targetCell)
    {
        if (CheckCellOnMove(targetCell))
        {
            list.Add(targetCell);
        }
    }

    protected void AddCellToListOnEat(List<Cell> list, Cell targetCell)
    {
        if (CheckCellOnEat(targetCell))
        {
            list.Add(targetCell);
        }
    }

    protected void AddCellToListOnMoveAndEat(List<Cell> list, Cell targetCell)
    {
        if (CheckCellOnMoveAndEat(targetCell))
        {
            list.Add(targetCell);
        }
    }
}
