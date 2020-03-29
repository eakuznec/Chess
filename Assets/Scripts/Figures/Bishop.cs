using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Figure
{
    public override List<Cell> GetAvaliableTargets()
    {
        return GetDiagonalCells();
    }
}
