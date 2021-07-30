using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnBrain
{
    Pathfinder pathfinder;
    PawnMovement pawnMovement;
    GameObject pawn;

    public PawnBrain()
    {
        pathfinder = new Pathfinder(GlobalGrid.GetGrid());
        pawn = GameObject.Find("Capsule");
        pawnMovement = pawn.GetComponent<PawnMovement>();

    }

    public void Roam()
    {
        pawnMovement.SetPath(pathfinder.Pathfind(pawn.transform.position, new Vector3(pawn.transform.position.x + Random.Range(-5, 5), pawn.transform.position.y, pawn.transform.position.z + Random.Range(-5, 5))));
    }
}
