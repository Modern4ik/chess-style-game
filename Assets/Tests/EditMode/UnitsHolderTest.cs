using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using System;

public class UnitsHolderTest
{
    private UnitFactory _unitFactory = new UnitFactory(new UnitPrefabLoaderStub());
    
    [Test]
    public void CheckSingleElement()
    {
        UnitsHolder unitsHolder = new UnitsHolderImpl();
    
        var enemy = _unitFactory.createUnit(Faction.Enemy);
        
        unitsHolder.AddUnit(enemy);
        IEnumerable<BaseUnit> enumerable = unitsHolder.GetAllUnits();
    
        var result = enumerable.ToList();
    
        Assert.AreEqual(enemy, result.First());
    }
    
    [Test]
    public void DeleteElement()
    {
        UnitsHolder unitsHolder = new UnitsHolderImpl();
    
        var enemy1 = _unitFactory.createUnit(Faction.Enemy);
        var enemy2 = _unitFactory.createUnit(Faction.Enemy);
        var enemy3 = _unitFactory.createUnit(Faction.Enemy);
        
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
    
    
}
