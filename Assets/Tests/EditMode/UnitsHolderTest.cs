using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using System;

public class UnitsHolderTest
{
    [Test]
    public void CheckSingleElement()
    {
        UnitsHolder unitsHolder = new UnitsHolderImpl();

        var enemy = createUnit(Faction.Enemy);
        
        unitsHolder.AddUnit(enemy);
        IEnumerable<BaseUnityUnit> enumerable = unitsHolder.GetAllUnits();

        var result = enumerable.ToList();

        Assert.AreEqual(enemy, result.First());
    }
    
    [Test]
    public void DeleteElement()
    {
        UnitsHolder unitsHolder = new UnitsHolderImpl();

        var enemy1 = createUnit(Faction.Enemy);
        var enemy2 = createUnit(Faction.Enemy);
        var enemy3 = createUnit(Faction.Enemy);
        
        unitsHolder.AddUnit(enemy1);
        unitsHolder.AddUnit(enemy2);
        unitsHolder.AddUnit(enemy3);
        IEnumerable<BaseUnityUnit> enumerable = unitsHolder.GetAllUnits();
        var seen = new List<BaseUnityUnit>(){};
        foreach (BaseUnityUnit unit in enumerable)
        {
            unitsHolder.DeleteUnit(enemy2);
            seen.Add(unit);
        }
        
        var expected = new List<BaseUnityUnit>(){enemy1, enemy3};

        var result = enumerable.ToList();

        Assert.AreEqual(expected, seen);
    }

    private BaseUnityUnit createUnit(Faction faction)
    {
        GameObject gameObject = new GameObject();
        switch (faction)
        {
            case Faction.Hero:
                gameObject.AddComponent<Hero1>();
                Hero1 hero1 = gameObject.GetComponent<Hero1>();
                return hero1;
            case Faction.Enemy:
                gameObject.AddComponent<Enemy1>();
                Enemy1 enemy1 = gameObject.GetComponent<Enemy1>();
                return enemy1;
        }

        throw new Exception("unknown faction");
    }
    
    //ScriptableUnit testObj = ScriptableObject.CreateInstance<ScriptableUnit>();
    //testObj.Faction = Faction.Enemy;
    
}
