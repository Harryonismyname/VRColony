using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PawnManager pawnManager;
    GridMap<MapNode> map;
    GridMapDebugger mapDebugger;
    [SerializeField] int height;
    [SerializeField] int width;
    [SerializeField] int depth;
    [SerializeField] float cellSize;
    [SerializeField] Transform mapSpawnPoint;
    [SerializeField] bool debugging;
    // Start is called before the first frame update
    void Start()
    {
        GlobalGrid.GenerateGrid(height, width, depth, cellSize, mapSpawnPoint.position);
        map = GlobalGrid.GetGrid();
        if (map == null)
        {
            while(map == null)
            {
                map = GlobalGrid.GetGrid();
            }
        }
        if (debugging)
        {
            mapDebugger = new GridMapDebugger();
            Debug();
        }
        pawnManager = new PawnManager();
        
    }

    //new GridMap<MapNode>(height, width, depth, cellSize, mapSpawnPoint.position, (GridMap<MapNode> g, int x, int y, int z) => new MapNode(g, x, y, z), true);

    // Update is called once per frame
    void Update()
    {
        if(pawnManager != null)
        {
            pawnManager.MakePawnsThink();
        }
        
    }
     public void Debug()
    {
        mapDebugger.RenderDebug();
    }
}
