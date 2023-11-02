using System.Collections.Generic;
using UnityEngine;

public class Bishop : BaseUnit
{
    public Bishop(string name, Faction faction, MonoBehaviour monoBehaviour) : base(bishopName + name, faction, 2, 1, _movePattern, monoBehaviour) { }
    private static string bishopName = "Bishop ";
    private static MovePattern _movePattern = new MovePattern(
        new List<List<Coordinate>>()
        {
            new List<Coordinate>()
            {
                new Coordinate(1, 1),
                new Coordinate(1, 1)
            },
            new List<Coordinate>()
            {
                new Coordinate(-1, 1),
                new Coordinate(-1, 1)
            }
        });

}
