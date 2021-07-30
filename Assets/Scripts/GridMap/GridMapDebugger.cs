using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMapDebugger
{
    GridMap<MapNode> gridMap;

    public GridMapDebugger()
    {
        gridMap = GlobalGrid.GetGrid();
    }
    public void RenderDebug()
    {
        for (int x = 0; x < gridMap.GetWidth(); x++)
        {
            for (int y = 0; y < gridMap.GetHeight(); y++)
            {
                for (int z = 0; z < gridMap.GetDepth(); z++)
                {
                    Debug.DrawLine(gridMap.GetWorldPosition(x, y, z), gridMap.GetWorldPosition(x, y + 1, z), Color.white, 100f);
                    Debug.DrawLine(gridMap.GetWorldPosition(x, y, z), gridMap.GetWorldPosition(x + 1, y, z), Color.white, 100f);
                    Debug.DrawLine(gridMap.GetWorldPosition(x, y, z), gridMap.GetWorldPosition(x, y, z + 1), Color.white, 100f);
                }
            }
        }
        /*Debug.DrawLine(gridMap.GetWorldPosition(0, gridMap.GetHeight(), 0), gridMap.GetWorldPosition(gridMap.GetWidth(), gridMap.GetHeight(), 0), Color.white, 100f);
        Debug.DrawLine(gridMap.GetWorldPosition(0, gridMap.GetHeight(), 0), gridMap.GetWorldPosition(0, gridMap.GetHeight(), gridMap.GetDepth()), Color.white, 100f);
        Debug.DrawLine(gridMap.GetWorldPosition(gridMap.GetWidth(), 0, 0), gridMap.GetWorldPosition(gridMap.GetWidth(), 0, gridMap.GetDepth()), Color.white, 100f);
        Debug.DrawLine(gridMap.GetWorldPosition(gridMap.GetWidth(), 0, 0), gridMap.GetWorldPosition(gridMap.GetWidth(), gridMap.GetHeight(), 0), Color.white, 100f);
        Debug.DrawLine(gridMap.GetWorldPosition(0, 0, gridMap.GetDepth()), gridMap.GetWorldPosition(0, gridMap.GetHeight(), gridMap.GetDepth()), Color.white, 100f);
        Debug.DrawLine(gridMap.GetWorldPosition(0, 0, gridMap.GetDepth()), gridMap.GetWorldPosition(gridMap.GetWidth(), 0, gridMap.GetDepth()), Color.white, 100f);*/
    }
    public void DebugCell(Vector3 location, Color color)
    {
        gridMap.GetXYZ(location, out int x, out int y, out int z);
        Vector3 leftCorner = gridMap.GetWorldPosition(x, y, z);
        Vector3 rightCorner = gridMap.GetWorldPosition(x, y, z);
        rightCorner.x += gridMap.cellSize + gridMap.originPosition.x;
        rightCorner.y += gridMap.cellSize + gridMap.originPosition.y;
        rightCorner.z += gridMap.cellSize + gridMap.originPosition.z;
        Debug.DrawLine(leftCorner, rightCorner, color, 100f);
    }
    public void DebugCell(int x, int y, int z, Color color)
    {
        Vector3 leftCorner = gridMap.GetWorldPosition(x, y, z);
        Vector3 rightCorner = gridMap.GetWorldPosition(x, y, z);
        rightCorner.x += gridMap.cellSize + gridMap.originPosition.x;
        rightCorner.y += gridMap.cellSize + gridMap.originPosition.y;
        rightCorner.z += gridMap.cellSize + gridMap.originPosition.z;
        Debug.DrawLine(leftCorner, rightCorner, color, 100f);
    }
}
