using UnityEngine;

public class UnitPrefabLoaderStub : IUnitPrefabLoader
{
    public override MonoBehaviour getRandomPrefab(Faction faction)
    {
        GameObject gameObject = new GameObject();
        gameObject.AddComponent<EmptyUnityObject>();
        EmptyUnityObject emptyUnityObject = gameObject.GetComponent<EmptyUnityObject>();
        return emptyUnityObject;
    }
}