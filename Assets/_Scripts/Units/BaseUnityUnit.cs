using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnityUnit : MonoBehaviour, BaseUnit
{
    public string UnitName { get; set; }
    public Tile OccupiedTile { get; set; }
    public Faction Faction { get; set; }

    public abstract Health getHealth();
    public abstract MovePattern movePattern();
    public abstract void ReceiveDamage(int damage);
}

