﻿using System;
using System.Collections.Generic;

public class Bishop : BaseUnityUnit
{
    private MovePattern _movePattern = new MovePattern(
        new List<Coordinate>() {
            new Coordinate(1, 1),
            new Coordinate(1, 1),
            new Coordinate(1, 1)
        }
      );

    public override MovePattern movePattern()
    {
        return _movePattern;
    }

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

