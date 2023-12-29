using UnityEngine;
using UserInput;
using GameLogic.Units;
using GameLogic.Factory;

namespace GameLogic
{
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
}