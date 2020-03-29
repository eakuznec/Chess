using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Figure
{
    public override List<Cell> GetAvaliableTargets()
    {
        Game game = Game.instance;
        List<Cell> retVal = new List<Cell>();
        int index = white ? 1 : -1;
        Cell targetCell = null;
        //Step forward
        targetCell = game.board.GetCell(x + index, y);
        if(CheckCellOnMove(targetCell))
        {
            retVal.Add(targetCell);
            //Step forward 2
            if (firstMove)
            {
                AddCellToListOnMove(retVal, game.board.GetCell(x + 2 * index, y));
            }
        }
        //DiagonalCells
        AddCellToListOnEat(retVal, game.board.GetCell(x + index, y - 1));
        AddCellToListOnEat(retVal, game.board.GetCell(x + index, y + 1));

        return retVal;
    }

    public override void MoveOnCell(Cell cell)
    {
        KillOrMove(cell);
        if ((white && cell.x == Game.instance.board.size - 1) || (!white && cell.x == 0))
        {
            InterfaceManager.instance.pawnPromotionPanel.Show(this);
        }
        else
        {
            EndTurn();
        }
    }
}
