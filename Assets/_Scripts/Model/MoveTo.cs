public class MoveTo : UnitMove
{
    public Coordinate step { get; }

    public MoveTo(Coordinate step, GameTile validTileToMove)
    {
        this.step = step;
        this.validTileToMove = validTileToMove;
    }
}