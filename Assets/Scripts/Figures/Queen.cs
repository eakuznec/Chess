using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Figure
{

    public override List<Cell> GetAvaliableTargets()
    {
        List<Cell> retVal = new List<Cell>();
        retVal.AddRange(GetLinearCells());
        retVal.AddRange(GetDiagonalCells());
        return retVal;
    }
}
