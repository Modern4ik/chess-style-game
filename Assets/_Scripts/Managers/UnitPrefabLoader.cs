using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class UnitPrefabLoader : IUnitPrefabLoader
{
    
    private List<ScriptableUnit> playerUnits;
    private List<ScriptableUnit> enemyUnits;

    public UnitPrefabLoader()
    {
        playerUnits = Resources.LoadAll<ScriptableUnit>("Units/Heroes").ToList();
        enemyUnits = Resources.LoadAll<ScriptableUnit>("Units/Enemies").ToList();
    }

    public override MonoBehaviour getUnitPrefab(Faction faction)
    {   
        if (faction == Faction.Hero)
        {
            var prefab = playerUnits.Find(pref => pref.UnitPrefab.tag == Tile.droppedUnitTag).UnitPrefab;
            MonoBehaviour instance = UnityEngine.Object.Instantiate(prefab);
            return instance;
        }
        else
        {
            var prefab = selectScriptableUnits(faction).OrderBy(o => Random.value).First().UnitPrefab;
            MonoBehaviour instance = UnityEngine.Object.Instantiate(prefab);
            return instance;
        }  
    }

    private List<ScriptableUnit> selectScriptableUnits(Faction faction)
    {
        switch (faction)
        {
            case Faction.Hero:
                return playerUnits;
            case Faction.Enemy:
                return enemyUnits;
            default:
                throw new NotImplementedException($"Unknown faction {faction}");
        }
    }
}
