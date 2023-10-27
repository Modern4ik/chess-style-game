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

        switch (prefab.tag)
        {
            case "Horse": return new Horse("new_horse", faction, prefab);
            case "Bishop": return new Bishop("new_bishop", faction, prefab);
            case "Pawn": return new Pawn("new_pawn", faction, prefab);

            default:
                throw new Exception($"Unexpected prefab tag: {prefab.tag}");
        }
    }
}
