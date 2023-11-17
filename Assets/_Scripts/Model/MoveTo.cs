public class MoveTo : UnitMove
{
    public Coordinate step { get; }

    public MoveTo(Coordinate step, Tile validTileToMove)
    {
        this.step = step;
        this.validTileToMove = validTileToMove;
    }
}