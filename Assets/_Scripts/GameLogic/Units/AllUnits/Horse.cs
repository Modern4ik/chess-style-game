using System.Collections.Generic;
using GameLogic.Factory;

namespace GameLogic
{
    namespace Units
    {
        public class Horse : BaseUnit
        {
            public Horse(string name, Faction faction, UnitSettings unitSettings) : base(horseName + name, faction, 2, 2, _movePattern, unitSettings) { }
            private static string horseName = "Horse ";
            private static MovePattern _movePattern = new MovePattern(
                new List<List<Coordinate>>() {
            new List<Coordinate>()
            {
                new Coordinate(1, 2)
            },
            new List<Coordinate>()
            {
                new Coordinate(-1, 2)
            }
                }
              );
        }
    }
}