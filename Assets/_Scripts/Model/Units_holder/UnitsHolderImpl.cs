using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

/*
 * Класс не потоко-безопасный
 * Использовать только в последовательной логике
 */

public class UnitsHolderImpl : UnitsHolder
{

    /*
     * Зачем так сложно.
     * Если из List-a удалять элементы, пока мы по нему итерируемся, получим ошибку от итератора.
     * Здесь мы заимплементили итератор, который не допускает подобную ошибку,
     * в случае если мы работаем с ним в одном потоке.
     */

    private List<AliveUnitWrapper> units = new List<AliveUnitWrapper>() { };

    public void AddUnit(BaseUnityUnit baseUnit)
    {
        units.Add(new AliveUnitWrapper(baseUnit));
    }

    
    public void DeleteUnit(BaseUnityUnit baseUnit)
    {
        //TODO: надо переписать на безопасный вариант, который не может выбросить Exception. Есть ли в C# аналог Option?
        AliveUnitWrapper unitNeedToDelete = units.Find(unit => unit.getUnit() == baseUnit);
        unitNeedToDelete.MarkForDeletion();
    }

    public IEnumerable<BaseUnityUnit> GetAllUnits()
    {
        return units.Where(unit => unit.isAlive()).Select(unit => unit.getUnit());
    }

    public IEnumerable<BaseUnityUnit> GetUnits(Faction faction)
    {
        return GetAllUnits().Where(unit => unit.Faction == faction);
    }

    public void compact()
    {
        units.RemoveAll(unit => unit.isAlive() == false);
    }

    
}

