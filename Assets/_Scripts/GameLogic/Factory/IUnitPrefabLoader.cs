using UnityEngine;
using UserInput;
using GameLogic.Units;

namespace GameLogic
{   
    namespace Factory
    {
        public abstract class IUnitPrefabLoader
        {
            public abstract MonoBehaviour getUnitPrefab(Faction faction, InputData inputData);

        }
    }
}
