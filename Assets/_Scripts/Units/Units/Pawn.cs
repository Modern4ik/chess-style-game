using System;
using System.Collections.Generic;

public class Pawn : BaseUnityUnit
{
    private MovePattern _movePattern = new MovePattern(
        new List<Coordinate>() {
            new Coordinate(0, 1)
        }
      );

    public override MovePattern movePattern()
    {
        return _movePattern;
    }

    //TODO: может есть смысл вынести выше по иерархии?
    private Health health = new Health(2);

    public override Health getHealth()
    {
        return health;
    }

    public override void ReceiveDamage(int damage)
    {
        health.GetDamage(1);
    }
}

