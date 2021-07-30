using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridMap<TGridObject>
{
    private readonly int height;
    private readonly int width;
    private readonly int depth;
    public float cellSize;
    public Vector3 originPosition;
    private TGridObject[,,] gridArray;

    public GridMap(int height, int width, int depth, float cellSize, Vector3 originPosition, Func<GridMap<TGridObject>, int, int, int, TGridObject> createGridObject)
    {
        this.height = height;
        this.width = width;
        this.depth = depth;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height, depth];

        for(int x = 0; x<gridArray.GetLength(0); x++)
        {
            for(int y = 0; y < gridArray.GetLength(1); y++) 
            {
                for (int z = 0; z < gridArray.GetLength(2); z++)
                {
                    gridArray[x, y, z] = createGridObject(this, x, y, z);
                }
                
            }
        }
    }
    // Setters
    public void SetGridObject(int x, int y, int z, TGridObject value)
    {
        if (GridMapValidator.IsValidCell(x, y, z))
        {
            gridArray[x, y, z] = value;
        }
    }
    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        GetXYZ(worldPosition, out int x, out int y, out int z);
        SetGridObject(x, y, z, value);
    }
    // Getters
    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }

    public int GetDepth()
    {
        return depth;
    }
    public TGridObject GetGridObject(int x, int y, int z)
    {
        if (GridMapValidator.IsValidCell(x, y, z))
        {
            return gridArray[x, y, z];
        }
        else
        {
            return default;
        }
    }
    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        GetXYZ(worldPosition, out int x, out int y, out int z);
        return GetGridObject(x, y, z);
    }

    public Vector3 GetCellCenterWorld(Vector3 position)
    {
        GetXYZ(position, out int x, out int y, out int z);
        Vector3 location = GetCellCenter(GetWorldPosition(x, y, z));
        location += originPosition;
        return location;
    }
    
    public Vector3 GetCellCenterWorld(int x, int y, int z)
    {
        Vector3 location = GetCellCenter(GetWorldPosition(x, y, z));
        return location;
    }

    private Vector3 GetCellCenter(Vector3 location)
    {
        location.x += (cellSize / 2);
        location.y += (cellSize / 2);
        location.z += (cellSize / 2);
        return location;
    }

    public Vector3 GetWorldPosition(int x, int y, int z)
    {
        if (x > 0 && y > 0 && z > 0)
        {
            if (x < width && y < height && z < depth)
            {
                return new Vector3(x, y, z) * cellSize + originPosition;
            }
            else return new Vector3(width, height, depth);
        } 
        else
        {
            return originPosition;
        }
    }
    public void GetXYZ(Vector3 worldPosition, out int x, out int y, out int z)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        y = Mathf.FloorToInt(worldPosition.y / cellSize);
        z = Mathf.FloorToInt(worldPosition.z / cellSize);
    }
}
