using UnityEngine;

public abstract class IUnitPrefabLoader
{
    public abstract MonoBehaviour getUnitPrefab(Faction faction, InputData inputData);
    
}