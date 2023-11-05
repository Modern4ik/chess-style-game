using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

public class UnitsHolderTest
{
    [Test]
    public void CheckSingleElement()
    {
        UnitsHolder unitsHolder = new UnitsHolderImpl();
    
        var enemy = new Pawn("test pawn 1", Faction.Enemy, emptyUnitSettings());
        
        unitsHolder.AddUnit(enemy);
        IEnumerable<BaseUnit> enumerable = unitsHolder.GetAllUnits();
    
        var result = enumerable.ToList();
    
        Assert.AreEqual(enemy, result.First());
    }
    
    [Test]
    public void DeleteElement()
    {
        UnitsHolder unitsHolder = new UnitsHolderImpl();

        var enemy1 = new Pawn("test pawn 1", Faction.Enemy, emptyUnitSettings());
        var enemy2 = new Pawn("test pawn 2", Faction.Enemy, emptyUnitSettings());
        var enemy3 = new Pawn("test pawn 3", Faction.Enemy, emptyUnitSettings());
        
        unitsHolder.AddUnit(enemy1);
        unitsHolder.AddUnit(enemy2);
        unitsHolder.AddUnit(enemy3);
        IEnumerable<BaseUnit> enumerable = unitsHolder.GetAllUnits();
        var seen = new List<BaseUnit>(){};
        foreach (BaseUnit unit in enumerable)
        {
            unitsHolder.DeleteUnit(enemy2);
            seen.Add(unit);
        }
        
        var expected = new List<BaseUnit>(){enemy1, enemy3};
    
        var result = enumerable.ToList();
    
        Assert.AreEqual(expected, seen);
    }

    private UnitSettings emptyUnitSettings()
    {
        return new UnitSettings(new UnityObjectStub(), new HealthViewStub(), new Element());
    }
}
