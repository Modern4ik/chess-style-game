﻿using System.Collections.Generic;
using GameLogic.Units;

namespace GameLogic
{
    namespace Holders
    {
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
            public void AddUnit(BaseUnit baseUnit);
            public void DeleteUnit(BaseUnit baseUnit);
            public IEnumerable<BaseUnit> GetAllUnits();
            public IEnumerable<BaseUnit> GetUnits(Faction faction);
            public void compact();

        }
    }
}