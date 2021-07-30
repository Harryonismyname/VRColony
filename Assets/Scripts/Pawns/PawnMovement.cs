using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMovement : MonoBehaviour
{
    private List<Vector3> path;
    private Vector3 targetPosition;
    private int i;
    private readonly float MOVESPEED = 1.5f;

    private void Start()
    {
        i = 0;
        path = null;
        targetPosition = transform.position;
    }
    private void FixedUpdate()
    {
        if (path != null)
        {
            MoveAlongPath();
        }
        transform.position = GlobalGrid.GetGrid().GetCellCenterWorld(transform.position);
    }

    private void MoveAlongPath()
    {
        if (path != null)
        {

            if (i < path.Count - 1)
            {
                targetPosition = path[i];

            }
            if (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, MOVESPEED);
            }
            else if (transform.position == targetPosition && targetPosition != path[path.Count - 1])
            {
                if (i < path.Count - 1)
                {
                    i++;
                    targetPosition = path[i];
                }
            }
        }
    }
    public void SetPath(List<Vector3> newPath)
    {
        path = newPath;
        i = 0;
        targetPosition = path[i];
    }
    /*public void GeneratePathTo(Vector3 position)
    {
        Vector3 mapBounds = map.GetCellCenterWorld(new Vector3(map.GetWidth(), map.GetHeight(), map.GetDepth()));
        if (position.y < mapBounds.y && position.x < mapBounds.x && position.y > 0f && position.x > 0f)
        {
            List<Vector3> temp = pathfinder.Pathfind(location, position);
            if (temp.Count > 0)
            {
                SetPath(temp);
            }
        }
    }
    private void RandomRoamingDestination()
    {
        GeneratePathTo(new Vector3(Random.Range(0, map.GetWidth()), Random.Range(0, map.GetHeight()), Random.Range(0, map.GetDepth())));
    }*/
}
