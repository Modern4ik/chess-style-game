using NUnit.Framework;
using UnityEngine;

public class UnitLogicTest
{
    
    private IUnitFactory _unitFactory = new UnitFactoryStub();
    
    [Test]
    public void MoveHeroPawn()
    {
        var gameManager = new GameManagerStub();
        var gridManager = new GridManagerStub();
        var menuManager = new MenuManagerStub();
        var unitLogic = new UnitLogic(gridManager, _unitFactory);
        var spawnTile = gridManager.GetTileAtPosition(new Vector2(0, 0));
        BaseUnit hero = unitLogic.SpawnHero(new InputData { tileToSpawn = spawnTile}); 
        unitLogic.MoveUnits(Faction.Hero);
        Assert.AreEqual(0, hero.OccupiedTile.x);
        Assert.AreEqual(1, hero.OccupiedTile.y);
    }
    
    [Test]
    public void MoveEnemyPawn()
    {
        var gameManager = new GameManagerStub();
        var gridManager = new GridManagerStub();
        var menuManager = new MenuManagerStub();
        var unitLogic = new UnitLogic(gridManager, _unitFactory);
    
        var spawnTile = gridManager.GetTileAtPosition(new Vector2(0, 7));
        var enemy = unitLogic.SpawnUnit(Faction.Enemy, new InputData { tileToSpawn = spawnTile });
        unitLogic.MoveUnits(Faction.Enemy);
        Assert.AreEqual(0, enemy.OccupiedTile.x);
        Assert.AreEqual(6, enemy.OccupiedTile.y);
    }
    
    private UnitSettings emptyUnitSettings()
    {
        return new UnitSettings(new UnitViewStub(), new HealthViewStub(), new ElementalType());
    }
}
