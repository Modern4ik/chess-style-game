using System.Collections.Generic;
using GameLogic.Factory;

namespace GameLogic
{
    namespace Units
    {
        public class Bishop : BaseUnit
        {
            public Bishop(string name, Faction faction, UnitSettings unitSettings) : base(bishopName + name, faction, 2, 2, _movePattern, unitSettings) { }
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
    }
}