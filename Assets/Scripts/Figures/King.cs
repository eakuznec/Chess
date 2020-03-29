using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Figure
{
    public override List<Cell> GetAvaliableTargets()
    {
        Game game = Game.instance;
        List<Cell> retVal = new List<Cell>();
        foreach(Cell neigbornCell in game.board.GetNeighbourCells(cell))
        {
            AddCellToListOnMoveAndEat(retVal, neigbornCell);
        }

        Cell castlingCell = GetCastlingCell(x, 0);
        if (castlingCell)
        {
            retVal.Add(castlingCell);
        }
        castlingCell = GetCastlingCell(x, game.board.size - 1);
        if (castlingCell)
        {
            retVal.Add(castlingCell);
        }
        return retVal;
    }

    Cell GetCastlingCell(int rookX, int rookY)
    {
        if (firstMove)
        {
            Board board = Game.instance.board;
            Cell castlingCell = board.GetCell(rookX, rookY);
            Rook rook = castlingCell.figure as Rook;
            if (rook != null && rook.firstMove)
            {
                if (y - rook.y > 0)
                {
                    for (int i = rook.y + 1; i < y; i++)
                    {
                        if (board.GetCell(x, i).figure != null)
                        {
                            return null;
                        }
                    }
                    return board.GetCell(x, y - 2);
                }
                else
                {
                    for (int i = y + 1; i < rook.y; i++)
                    {
                        if (board.GetCell(x, i).figure != null)
                        {
                            return null;
                        }
                    }
                    return board.GetCell(x, y + 2);
                }
            }
            return null;
        }
        return null;
    }

    public override void Kill()
    {
        base.Kill();
        Game.instance.EndGame(!white);
    }

    public override void MoveOnCell(Cell cell)
    {
        Game game = Game.instance;
        if (GetCastlingCell(x, 0) == cell)
        {
            Rook rook = game.GetFigureOnPosition(x, 0) as Rook;
            rook.SetPosition(x, y - 1);
        }
        else if (GetCastlingCell(x, game.board.size - 1) == cell)
        {
            Rook rook = game.GetFigureOnPosition(x, game.board.size - 1) as Rook;
            rook.SetPosition(x, y + 1);
        }
        base.MoveOnCell(cell);
    }
}
