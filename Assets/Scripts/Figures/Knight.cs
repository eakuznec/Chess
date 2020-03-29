using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Figure
{
    public override List<Cell> GetAvaliableTargets()
    {
        Game game = Game.instance;
        List<Cell> retVal = new List<Cell>();
        AddCellToListOnMoveAndEat(retVal, game.board.GetCell(cell.x+2, cell.y +1));
        AddCellToListOnMoveAndEat(retVal, game.board.GetCell(cell.x + 2, cell.y - 1));
        AddCellToListOnMoveAndEat(retVal, game.board.GetCell(cell.x - 2, cell.y + 1));
        AddCellToListOnMoveAndEat(retVal, game.board.GetCell(cell.x - 2, cell.y - 1));
        AddCellToListOnMoveAndEat(retVal, game.board.GetCell(cell.x + 1, cell.y + 2));
        AddCellToListOnMoveAndEat(retVal, game.board.GetCell(cell.x + 1, cell.y - 2));
        AddCellToListOnMoveAndEat(retVal, game.board.GetCell(cell.x - 1, cell.y + 2));
        AddCellToListOnMoveAndEat(retVal, game.board.GetCell(cell.x - 1, cell.y - 2));

        return retVal;
    }
}
