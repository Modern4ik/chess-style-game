using System.Collections.Generic;
using UnityEngine;

public class Bishop : BaseUnit
{
    public Bishop(string name, Faction faction, MonoBehaviour monoBehaviour) : base(bishopName + name, faction, new Health(2), 1, _movePattern, monoBehaviour) { }
    private static string bishopName = "Bishop ";
    private static MovePattern _movePattern = new MovePattern(
        new List<Coordinate>() {
            new Coordinate(2, 2)
        }
      );

}
