using System.Collections.Generic;

public class UnitMove
{
    public Coordinate step{ get; }

    public Tile validTileToMove { get; set; }
    public bool isAttackOpponentMainHealth { get; set; }
    public bool isAttackHeroMainHealth { get; set; }

    public UnitMove(Coordinate step)
    {
        this.step = step;
        this.isAttackOpponentMainHealth = false;
        this.isAttackHeroMainHealth = false;
    }
}