using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn
{
    PawnBrain brain;
    public Pawn()
    {
        brain = new PawnBrain();
    }
    public void Think()
    {
        brain.Roam();
    }
}
