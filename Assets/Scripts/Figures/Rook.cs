using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Figure
{
    public override List<Cell> GetAvaliableTargets()
    {
        return GetLinearCells();
    }
}
