using System;
using System.Collections.Generic;
using UnityEngine;

public class Horse : BaseUnit
{
    public Horse(string name, Faction faction, MonoBehaviour monoBehaviour) : base(horseName + name, faction, new Health(2), 1, _movePattern, monoBehaviour) { }
    private static string horseName = "Horse ";
    private static MovePattern _movePattern = new MovePattern(
        new List<Coordinate>() {
            new Coordinate(1, 2)
        }
      );
}

