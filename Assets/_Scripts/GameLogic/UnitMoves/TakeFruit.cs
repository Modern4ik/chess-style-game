namespace GameLogic
{
    namespace UnitMoves
    {
        public class TakeFruit : UnitMove
        {
            public Coordinate step { get; }

            public TakeFruit(Coordinate step, GameTile validTileToMove)
            {
                this.step = step;
                this.validTileToMove = validTileToMove;
            }
        }
    }
}