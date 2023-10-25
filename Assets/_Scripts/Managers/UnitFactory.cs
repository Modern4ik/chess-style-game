using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class UnitFactory
{
    private IUnitPrefabLoader _unitPrefabLoader;
    public UnitFactory(IUnitPrefabLoader unitPrefabLoader)
    {
        this._unitPrefabLoader = unitPrefabLoader;
    }

    public BaseUnit createUnit(Faction faction)
    {
        MonoBehaviour prefab = _unitPrefabLoader.getRandomPrefab(faction);
        return new Pawn("new_pawn", faction, prefab);
    }
}
