using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager
{
    Pawn[] pawns;
    int numberOfPawns;

    public PawnManager()
    {
        numberOfPawns = 1;
        pawns = new Pawn[numberOfPawns];
        for(int i=0; i<numberOfPawns; i++)
        {
            pawns[i] = new Pawn();
        }
    }

    public void MakePawnsThink()
    {
        foreach (Pawn pawn in pawns)
        {
            pawn.Think();
        }
    }
}
