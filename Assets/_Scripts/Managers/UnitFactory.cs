using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class UnitFactory
{
    private List<ScriptableUnit> playerUnits;
    private List<ScriptableUnit> enemyUnits;

    public UnitFactory()
    {
        playerUnits = Resources.LoadAll<ScriptableUnit>("Units/Heroes").ToList();
        enemyUnits = Resources.LoadAll<ScriptableUnit>("Units/Enemies").ToList();
    }

    public BaseUnit spawnUnit<T>(Faction faction) where T : BaseUnit
    {
        switch (faction)
        {
            case Faction.Hero:
                EmptyUnityObject heroPrefab = playerUnits.OrderBy(o => Random.value).First().UnitPrefab;
                MonoBehaviour heroPrefabInstance = UnityEngine.Object.Instantiate(heroPrefab);
                return new Pawn("new_pawn", Faction.Hero, heroPrefabInstance);
            case Faction.Enemy:
                EmptyUnityObject enemyPrefab = enemyUnits.OrderBy(o => Random.value).First().UnitPrefab;
                MonoBehaviour enemyPrefabInstance = UnityEngine.Object.Instantiate(enemyPrefab);
                return new Pawn("new_pawn", Faction.Enemy, enemyPrefabInstance);
            default:
                throw new NotImplementedException($"Unknown faction {faction}");
        }
    }
}
