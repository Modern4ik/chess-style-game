using System.Collections.Generic;
using GameLogic.Factory;

namespace GameLogic
{
    namespace Units
    {
        public class Pawn : BaseUnit
        {
            public Pawn(string name, Faction faction, UnitSettings unitSettings) : base(pawnName + name, faction, 2, 2, _movePattern, unitSettings) { }
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
    }
}