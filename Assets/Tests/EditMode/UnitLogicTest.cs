using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using System;

public class UnitLogicTest
{
    [Test]
    public void MoveHeroPawn()
    {
        var gameManager = new GameManagerStub();
        var gridManager = new GridManagerStub();
        var menuManager = new MenuManagerStub();
        var unitLogic = new UnitLogic(gridManager, gameManager, menuManager);
        var spawnTile = gridManager.GetTileAtPosition(new Vector2(0, 0));
        var spawnUnit = UnityUnitCreator.createUnit(Faction.Hero);
        //По хорошему нужно передать юнит какого-то типа
        //Или конкретный тип юнита
        BaseUnityUnit hero = unitLogic.SpawnUnit(spawnTile, spawnUnit); 
        unitLogic.MoveUnits(Faction.Hero);
        Assert.AreEqual(0, hero.OccupiedTile.x);
        Assert.AreEqual(1, hero.OccupiedTile.y);
    }

    [Test]
    public void MoveEnemyPawn()
    {
        
    }

    [Test]
    public void KillEnemyPawn()
    {

    }
}
