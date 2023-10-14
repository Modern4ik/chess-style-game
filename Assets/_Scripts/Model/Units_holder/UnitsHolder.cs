using System;
using System.Collections.Generic;

public interface UnitsHolder
{
    /*
     * Нужна структура из которой:
     * удобно достать список своих/чужих юнитов, для того чтобы запустить логику.
     * (?) или они должны одновременно двигаться? Сейчас оставлю по очереди
     * Нужно иметь возможность достать всех юнитов
     * Нужно иметь возможность достать юнитов по Faction. Причём в виде BaseUnit
     * Удаление из структур данных должно быть простое. 'Удалить X' - и он будет удалён из всех структур.
     * Если понадобится, можно добавить безопасный итератор, чтобы в нём нельзя было наткнуться на удалённого юнита.
     */

    /*
     * Структура не потоко-безопасная. Т.е предполагается что обработка действий юнитов
     * будет по очереди
     */
    public void AddUnit(BaseUnityUnit baseUnit);
    public void DeleteUnit(BaseUnityUnit baseUnit);
    public IEnumerable<BaseUnityUnit> GetAllUnits();
    public IEnumerable<BaseUnityUnit> GetUnits(Faction faction);
    public void compact();

}