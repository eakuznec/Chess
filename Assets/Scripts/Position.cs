using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Position
{
    public int x;
    public int y;

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static bool operator == (Position first, Position second)
    {
        return (first.x == second.x) && (first.y == second.y);
    }

    public static bool operator !=(Position first, Position second)
    {
        return (first.x != second.x) || (first.y != second.y);
    }

    public override string ToString()
    {
        return string.Format("({0}:{1})", x, y);
    }
}
