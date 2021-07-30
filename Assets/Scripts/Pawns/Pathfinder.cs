using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private GridMap<MapNode> grid;
    private List<MapNode> openList;
    private List<MapNode> closedList;
    public Pathfinder(GridMap<MapNode> grid)
    {
        this.grid = grid;
    }

    public List<Vector3> Pathfind(Vector3 origin, Vector3 target)
    {
        target.z = 0;
        List<Vector3> worldPath = new List<Vector3>();
        grid.GetXYZ(origin, out int Ox, out int Oy, out int Oz);
        grid.GetXYZ(target, out int Tx, out int Ty, out int Tz);
        List<MapNode> path = FindPath(Ox, Oy, Oz, Tx, Ty, Tz);
        Debug.Log(path);
        if (path != null)
        {
            Vector3 prevNode = origin;
            foreach (MapNode node in path)
            {
                Vector3 temp = grid.GetCellCenterWorld(new Vector3(node.x, node.y, node.z));
                Debug.DrawLine(prevNode, temp, Color.green, 3f);
                worldPath.Add(grid.GetCellCenterWorld(new Vector3(node.x, node.y, node.z)));
                prevNode = temp;

            }
            Debug.Log("Found A Path!");
            return worldPath;
        }
        else
        {
            Debug.Log("Could Not Find A Path!");
            return new List<Vector3>();
        }
    }

    private List<MapNode> FindPath(int startX, int startY, int startZ, int endX, int endY, int endZ)
    {
        if (NullNodeCheck(startX, startY, startZ))
        {

            if (NullNodeCheck(endX, endY, endZ))
            {
                MapNode startNode = grid.GetGridObject(startX, startY, startZ);
                MapNode endNode = grid.GetGridObject(endX, endY, endZ);
                openList = new List<MapNode> { startNode };
                startNode.gCost = 0;
                startNode.hCost = CalculateDistance(startNode, endNode);
                startNode.CalculateFCost(); closedList = new List<MapNode>();
                for (int x = 0; x < grid.GetWidth(); x++)
                {
                    for (int y = 0; y < grid.GetHeight(); y++)
                    {
                        for (int z = 0; z < grid.GetDepth(); z++)
                        {
                            MapNode mapNode = grid.GetGridObject(x, y, z);
                            mapNode.gCost = int.MaxValue;
                            mapNode.CalculateFCost();
                            mapNode.cameFromNode = null;
                        }
                    }
                }


                while (openList.Count > 0)
                {
                    MapNode currentNode = GetLowestFCostNode(openList);
                    if (currentNode == endNode)
                    {
                        return CalculatePath(endNode);
                    }
                    openList.Remove(currentNode);
                    closedList.Add(currentNode);
                    foreach (MapNode neighborNode in GetNeighborList(currentNode))
                    {
                        if (closedList.Contains(neighborNode)) continue;
                        if (!neighborNode.isWalkable)
                        {
                            closedList.Add(neighborNode);
                            continue;
                        }
                        int tentativeGCost = currentNode.gCost + CalculateDistance(currentNode, neighborNode);
                        if (tentativeGCost < neighborNode.gCost)
                        {
                            neighborNode.cameFromNode = currentNode;
                            neighborNode.gCost = tentativeGCost;
                            neighborNode.hCost = CalculateDistance(neighborNode, endNode);
                            neighborNode.CalculateFCost();

                            if (!openList.Contains(neighborNode))
                            {
                                openList.Add(neighborNode);
                            }
                        }
                    }
                }
            }

        }
        return null;
    }

    private List<MapNode> GetNeighborList(MapNode currentNode)
    {
        List<MapNode> neighborList = new List<MapNode>();
        // Left Search
        if (currentNode.x - 1 >= 0)
        {
            // Left
            if (NullNodeCheck(currentNode.x - 1, currentNode.y, currentNode.z))
            {
                neighborList.Add(GetNode(currentNode.x - 1, currentNode.y, currentNode.z));
            }
            // Left Down
            if (currentNode.y - 1 >= 0) neighborList.Add(GetNode(currentNode.x - 1, currentNode.y - 1, currentNode.z));
            // Left Up
            if (currentNode.y + 1 < grid.GetHeight()) neighborList.Add(GetNode(currentNode.x - 1, currentNode.y + 1, currentNode.z));
            // Left Out
            if (currentNode.z - 1 >= 0) neighborList.Add(GetNode(currentNode.x - 1, currentNode.y, currentNode.z - 1));
            // Left In
            if (currentNode.z + 1 < grid.GetDepth()) neighborList.Add(GetNode(currentNode.x - 1, currentNode.y, currentNode.z + 1));
        }
        // Right Search
        if (currentNode.x + 1 < grid.GetWidth())
        {
            // Right
            if (NullNodeCheck(currentNode.x + 1, currentNode.y, currentNode.z))
            {
                neighborList.Add(GetNode(currentNode.x + 1, currentNode.y, currentNode.z));
            }
            // Right Down
            if (currentNode.y - 1 >= 0) neighborList.Add(GetNode(currentNode.x + 1, currentNode.y - 1, currentNode.z));
            // Right Up
            if (currentNode.y + 1 < grid.GetHeight()) neighborList.Add(GetNode(currentNode.x + 1, currentNode.y + 1, currentNode.z));
            // Right Out
            if (currentNode.z - 1 >= 0) neighborList.Add(GetNode(currentNode.x + 1, currentNode.y, currentNode.z - 1));
            // Right In
            if (currentNode.z + 1 < grid.GetDepth()) neighborList.Add(GetNode(currentNode.x + 1, currentNode.y, currentNode.z + 1));

        }
        // Down
        if (currentNode.y - 1 >= 0)
        {
            if (NullNodeCheck(currentNode.x, currentNode.y - 1, currentNode.z))
            {
                neighborList.Add(GetNode(currentNode.x, currentNode.y - 1, currentNode.z));
            }

        }
        // Up
        if (currentNode.y + 1 < grid.GetHeight())
        {
            if (NullNodeCheck(currentNode.x, currentNode.y + 1, currentNode.z))
            {
                neighborList.Add(GetNode(currentNode.x, currentNode.y + 1, currentNode.z));
            }
        }
        // Out
        if (currentNode.z - 1 > 0)
        {
            if (NullNodeCheck(currentNode.x, currentNode.y, currentNode.z - 1))
            {
                neighborList.Add(GetNode(currentNode.x, currentNode.y, currentNode.z - 1));
            }
        }
        // In
        if (currentNode.z + 1 > 0)
        {
            if (NullNodeCheck(currentNode.x, currentNode.y, currentNode.z + 1))
            {
                neighborList.Add(GetNode(currentNode.x, currentNode.y, currentNode.z + 1));
            }
        }

        return neighborList;
    }
    private List<MapNode> CalculatePath(MapNode endNode)
    {
        List<MapNode> path = new List<MapNode>();
        path.Add(endNode);
        MapNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistance(MapNode a, MapNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int zDistance = Mathf.Abs(a.z - b.z);
        int remaining = Mathf.Abs(xDistance - yDistance - zDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance, zDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    // Getters
    private MapNode GetLowestFCostNode(List<MapNode> mapNodeList)
    {
        MapNode lowestFCostNode = mapNodeList[0];
        for (int i = 1; i < mapNodeList.Count; i++)
        {
            if (mapNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = mapNodeList[i];
            }
        }
        return lowestFCostNode;
    }

    public MapNode GetNode(int x, int y, int z)
    {
        MapNode node = grid.GetGridObject(x, y, z);
        if (node != null)
        {
            return node;
        }
        else return null;
    }
    public bool NullNodeCheck(int x, int y, int z)
    {
        if (GetNode(x, y, z) != null)
        {
            return true;
        }
        else return false;
    }
    public GridMap<MapNode> GetGrid()
    {
        return grid;
    }
}
