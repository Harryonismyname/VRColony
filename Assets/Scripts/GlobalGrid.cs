using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalGrid
{
    static GridMap<MapNode> grid;

    public static void GenerateGrid(int height, int width, int depth, float cellSize, Vector3 position)
    {
        grid = new GridMap<MapNode>(height, width, depth, cellSize, position, (GridMap<MapNode> g, int x, int y, int z) => new MapNode(g, x, y, z));
    }

    public static GridMap<MapNode> GetGrid()
    {
        if (grid != null)
        {
            return grid;
        }
        else return null;
    }
}
