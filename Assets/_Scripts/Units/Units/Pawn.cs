using System;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseUnit
{
    public Pawn(string name, Faction faction, MonoBehaviour monoBehaviour) : base(pawnName + name, faction, new Health(2), 1, _movePattern, monoBehaviour) { }
    private static string pawnName = "Pawn ";
    private static MovePattern _movePattern = new MovePattern(
        new List<List<Coordinate>>() {
            new List<Coordinate>()
            {
                new Coordinate(0, 1)
            }
        }
      );
}

