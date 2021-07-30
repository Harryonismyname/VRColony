using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode
{
    public int x;
    public int y;
    public int z;
    private GridMap<MapNode> map;

    public int hCost;
    public int gCost;
    public int fCost;

    public bool isWalkable;

    public MapNode cameFromNode;

    public MapNode(GridMap<MapNode> map, int x, int y, int z)
    {
        this.map = map;
        this.x = x;
        this.y = y;
        this.z = z;
        isWalkable = true;
    }

    public void CalculateFCost()
    {
        fCost = hCost + gCost;
    }
}
