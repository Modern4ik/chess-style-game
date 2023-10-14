using System.Collections;
using System.Collections.Generic;

using System;

public interface BaseUnit
{
    public abstract string UnitName { get; set; }
    public abstract Tile OccupiedTile { get; set; }
    public abstract Faction Faction { get; set; }
    public abstract MovePattern movePattern();
    public abstract Health getHealth();
    public abstract void ReceiveDamage(int damage);
}
