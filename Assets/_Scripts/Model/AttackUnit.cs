public class AttackUnit : UnitMove
{
    public Coordinate step { get; }

    public AttackUnit(Coordinate step, GameTile validTileToMove)
    {
        this.step = step;
        this.validTileToMove = validTileToMove;
    }
}