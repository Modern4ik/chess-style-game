using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            List<Coordinate> validSequance = GetValidSequance(unit.getMoveSequances(), unit);
            Debug.Log($"moved units {faction} {unit.getName()}");
            foreach (Coordinate step in validSequance)
            {
                //TODO: порефакторить так чтобы не нужно было передавать так много параметров
                Debug.Log($"{faction} {step.y}");
                bool doNextMovement = TryMoveOrFight(faction, unit, step);
                if (!doNextMovement) break;
            }
        }
        unitsHolder.compact();
    }

    //TODO: эту ф-ию явно можно упростить. Разбить на ф-ии попроще и часть унести в поведение юнита.
    private bool TryMoveOrFight(Faction faction, BaseUnit unit, Coordinate step)
    {   
        Tile occupiedTile = unit.OccupiedTile;
        int moveToY = occupiedTile.y + step.y;
        int moveToX = occupiedTile.x + step.x;
        //Определяем что делать, в зависимости от того что на следующем tile
        if (IsInTheEndZone(moveToY, faction))
        {
            menuManager.DoDamageToMainHero(faction);
            DestroyUnit(unit);
            occupiedTile.OccupiedUnit = null;
            return false;
        }
        else
        {
            Tile moveTo = gridManager.GetTileAtPosition(new Vector2(moveToX, moveToY));
            if (moveTo.OccupiedUnit != null) //Что делать, если кто-то уже есть на этом тайле
            {  
                //Это чужой юнит, нужно с ним сражаться
                return Fight(unit, moveTo.OccupiedUnit); //Юнит файтится 1 раз. Если пофайтился, дальше не двигается
            }
            else
            { //Клетка пустая, сдвигаемся
                moveTo.SetUnit(unit);
                return true;
            }
        }
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

    private bool Fight(BaseUnit attackingUnit, BaseUnit defendingUnit)
    {
        float remainingHealth = defendingUnit.receiveDamage(attackingUnit.getAtack()).GetCurrentHealth();

        if (remainingHealth <= 0)
        {
            DestroyUnit(defendingUnit);
            defendingUnit.OccupiedTile.SetUnit(attackingUnit);

            return true;
        }

        return false;
    }

    private void DestroyUnit(BaseUnit unit)
    {
        unitsHolder.DeleteUnit(unit);
        Object.Destroy(unit.getUnityObject().gameObject);
    }

    private List<Coordinate> GetValidSequance(List<List<Coordinate>> moveSequances, BaseUnit unit)
    {
        List<List<Coordinate>> validSequances = new List<List<Coordinate>>();

        foreach (List<Coordinate> sequance in moveSequances)
        {   
            List<Coordinate> steps = GetValidSteps(sequance, unit.OccupiedTile, unit.getFaction());

            if (steps.Count > 0) validSequances.Add(steps);
        }

        return GetRandomSequance(validSequances);
    }

    private List<Coordinate> GetValidSteps (List<Coordinate> sequance, Tile startTile, Faction faction)
    {
        List<Coordinate> validSteps = new List<Coordinate>();
        Tile currentTile = startTile;

        foreach(Coordinate coord in sequance)
        {
            int moveToX = currentTile.x + coord.x;
            int moveToY = currentTile.y + coord.y;
            Tile tileMoveTo = gridManager.GetTileAtPosition(new Vector2(moveToX, moveToY));

            if (CheckSideBorders(moveToX) && CheckTileOnAlly(tileMoveTo, faction))
            {
                if (CheckTileOnEnemy(tileMoveTo, faction))
                {
                    validSteps.Add(coord);
                    break;
                }
                validSteps.Add(coord);
            }
            else break;

            if (tileMoveTo != null)currentTile = tileMoveTo;
        }
        return validSteps;
    }

    private List<Coordinate> GetRandomSequance(List<List<Coordinate>> moveSequances)
    {
        if (moveSequances.Count > 0) return moveSequances.OrderBy(o => Random.value).First();

        else return new List<Coordinate>();
    }

    private bool CheckTileOnAlly(Tile tileMoveTo, Faction faction)
    {
        if (tileMoveTo == null) return true;

        if (tileMoveTo.OccupiedUnit != null && tileMoveTo.OccupiedUnit.getFaction() == faction)
        {
            return false;
        }

        return true;
    }

    private bool CheckTileOnEnemy(Tile tileMoveTo, Faction faction)
    {
        if (tileMoveTo == null) return false;

        if (tileMoveTo.OccupiedUnit != null && tileMoveTo.OccupiedUnit.getFaction() != faction)
        {
            return true;
        }

        return false;
    }

    private bool CheckSideBorders(int coordX)
    {
        if (coordX >= 0 && coordX < GridSettings.WIDTH) return true;

        return false;
    }
}
