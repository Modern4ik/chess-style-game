using System;
using UnityEngine;
using UserInput;
using GameLogic.Units;

namespace GameLogic
{   
    namespace Factory
    {
        public class UnitFactory : IUnitFactory
        {
            private IUnitPrefabLoader _unitPrefabLoader;
            public UnitFactory(IUnitPrefabLoader unitPrefabLoader)
            {
                this._unitPrefabLoader = unitPrefabLoader;
            }

            public BaseUnit createUnit(Faction faction, InputData inputData)
            {
                MonoBehaviour prefab = _unitPrefabLoader.getUnitPrefab(faction, inputData);
                UnitSettings unitSettings = UnitSettingsCreator.createUnitSettings(prefab);

                switch (prefab.tag)
                {
                    case "Horse": return new Horse("new_horse", faction, unitSettings);
                    case "Bishop": return new Bishop("new_bishop", faction, unitSettings);
                    case "Pawn": return new Pawn("new_pawn", faction, unitSettings);

                    default:
                        throw new Exception($"Unexpected prefab tag: {prefab.tag}");
                }
            }
        }
    } 
}

