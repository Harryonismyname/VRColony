using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridMapValidator
{
    static GridMap<MapNode> gridMap = GlobalGrid.GetGrid();
    public static bool IsValidCell(int x, int y, int z)
    {
        if (x >= 0 && y >= 0 && x <= gridMap.GetWidth() - 1 && y <= gridMap.GetHeight() - 1 && z >= 0 && z <= gridMap.GetDepth())
        {
            return true;
        }
        else return false;
    }
    public static bool IsValidCell(Vector3 location)
    {
        gridMap.GetXYZ(location, out int x, out int y, out int z);
        return IsValidCell(x, y, z);
    }
}
