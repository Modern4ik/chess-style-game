using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GameObject;

public class UnitLogic
{
        //Делаю так, чтобы разбить зависимость singleton-ов, для того чтобы код можно было потестить по частям.
        private IGridManager gridManager;
        private IGameManager gameManager;
        private IMenuManager menuManager;
      
        private UnitsHolder unitsHolder;
        private UnitFactory unitFactory;
        
        public UnitLogic(IGridManager gridManager, IGameManager gameManager, IMenuManager menuManager, IUnitPrefabLoader unitPrefabLoader)
        {
                this.gridManager = gridManager;
                this.gameManager = gameManager;
                this.menuManager = menuManager;
                
                unitFactory = new UnitFactory(unitPrefabLoader);
                unitsHolder = new UnitsHolderImpl();
        }

        // public BaseUnit SpawnUnit(Tile tile, BaseUnit unit)
        // {
        //     BaseUnit spawnedUnit = Object.Instantiate(unit);
        //     tile.SetUnit(spawnedUnit);
        //     unitsHolder.AddUnit(spawnedUnit);
        //     
        //     Faction faction = unit.Faction;
        //     switch (faction)
        //     {
        //         case Faction.Hero:
        //             gameManager.ChangeState(GameState.HeroesTurn);
        //             break;
        //         case Faction.Enemy:
        //             gameManager.ChangeState(GameState.EnemiesTurn);
        //             break;
        //     }
        //     
        //     return spawnedUnit;
        // }
        
    public BaseUnit SpawnHero(Tile tile)
    {
        var unit = SpawnUnit(Faction.Hero, tile);
        gameManager.ChangeState(GameState.HeroesTurn);
        return unit;
    }

    public void SpawnEnemies()
    { 
        var enemyCount = 1;

        for (int i = 0; i < enemyCount; i++)
        {
            var randomSpawnTile = gridManager.GetEnemySpawnTile();
            SpawnUnit(Faction.Enemy, randomSpawnTile);
        }

        gameManager.ChangeState(GameState.EnemiesTurn);
    }

    public BaseUnit SpawnUnit(Faction faction, Tile tile)
    {
        var unit = unitFactory.createUnit(faction);
        tile.SetUnit(unit);
        unitsHolder.AddUnit(unit);
        return unit;
    }

    public void MoveUnits(Faction faction)
    {
        IEnumerable<BaseUnit> unitsEnumerator = unitsHolder.GetUnits(faction);
        foreach (BaseUnit unit in unitsEnumerator)
        {
            List<Coordinate> movementSteps = factionDependentSteps(unit, faction);
            Debug.Log($"moved units {faction} {unit.getName()}");
            foreach (Coordinate step in movementSteps)
            {
                //TODO: порефакторить так чтобы не нужно было передавать так много параметров
                Debug.Log($"{faction} {step.y}");
                bool doNextMovement = TryMoveOrFight(faction, unit, step);
                if (!doNextMovement) break;
            }
            
        }
        unitsHolder.compact();
    }

    //TODO: может быть это инкапсулировать внутри unit.movePattern? Т.к у юнита уже известна фракция?
    private List<Coordinate> factionDependentSteps(BaseUnit unit, Faction faction)
    {
        int ySign = yMultiplier(faction);
        MovePattern movePattern = unit.getMovePattern();
        List<Coordinate> newSteps = movePattern.moveSequence.Select(coordinate => new Coordinate(coordinate.x, coordinate.y * ySign)).ToList();
        return newSteps;
    }

    //TODO: эту ф-ию явно можно упростить. Разбить на ф-ии попроще и часть унести в поведение юнита.
    private bool TryMoveOrFight(Faction faction, BaseUnit unit, Coordinate step)
    {
        Tile occupiedTile = unit.OccupiedTile;
        int moveToX = occupiedTile.x + step.x;
        int moveToY = occupiedTile.y + step.y;
        //Определяем что делать, в зависимости от того что на следующем tile
        if (IsInTheEndZone(moveToY, faction))
        {
            DoDamageToMainEnemy(faction);
            unitsHolder.DeleteUnit(unit);
            Object.Destroy(unit.getUnityObject().gameObject);
            occupiedTile.OccupiedUnit = null;
            return false;
        }
        else
        {
            Tile moveTo = gridManager.GetTileAtPosition(new Vector2(moveToX, moveToY));
            if (moveTo.OccupiedUnit != null) //Что делать, если кто-то уже есть на этом тайле
            {
                if (moveTo.OccupiedUnit.getFaction() == faction)
                { //Это союзный юнит, просто туда не идём, остаёмся где есть.
                    return false; //Юнит не смог сдвинуться, дальше не идёт.
                }
                else
                {
                    //Это чужой юнит, нужно с ним сражаться
                    //Можно сделать отдельную ф-ию fight. Сейчас просто удаляем чужого юнита.
                    unitsHolder.DeleteUnit(moveTo.OccupiedUnit);
                    Object.Destroy(moveTo.OccupiedUnit.getUnityObject().gameObject);
                    moveTo.SetUnit(unit);
                    return false; //Юнит файтится 1 раз. Если пофайтился, дальше не двигается
                }
            } else
            { //Клетка пустая, сдвигаемся
                moveTo.SetUnit(unit);
                return true;
            }
        }
    }


    //В зависимости от фракции юниты идут в разные стороны. Одни вниз, другие вверх. Для этого для врагов Y координату умножаю на 1
    private int yMultiplier(Faction faction)
    {
        int yMultiplier = 1;
        switch (faction)
        {
            case Faction.Hero:
                yMultiplier = 1;
                break;
            case Faction.Enemy:
                yMultiplier = -1;
                break;
        }
        return yMultiplier;
    }

    //Это можно вынести в модель фракции - какая куда стремится. Или в модель самого юнита зашить.
    private bool IsInTheEndZone(int y, Faction faction)
    {
        switch (faction)
        {
            case Faction.Hero:
                //TODO: размер доски должен быть в конструкторе класса.
                if (y > GridSettings.HEIGHT - 1) return true;
                else return false;
            case Faction.Enemy:
                if (y < 0) return true;
                else return false;
        }
        //Сюда никогда не должны попадать. Как в С# написать эту часть безопасно, чтобы не было Exception пока не понял.
        throw new System.Exception("в эту ветку кода никогда не должны попадать");
    }

    private void DoDamageToMainEnemy(Faction faction)
    {
        switch (faction)
        {
            case Faction.Hero:
                //TODO: это ещё нужно добавить
                break;
            case Faction.Enemy:
                menuManager.DamagePlayer(1);
                break;
        }
    }
}
