using UnityEngine;

public class UnitPrefabLoaderStub : IUnitPrefabLoader
{ 
    public override MonoBehaviour getUnitPrefab(Faction faction, InputData inputData)
    {
        GameObject gameObject = new GameObject();
        gameObject.AddComponent<EmptyUnityObject>();
        EmptyUnityObject emptyUnityObject = gameObject.GetComponent<EmptyUnityObject>();
        return emptyUnityObject;
    }
}