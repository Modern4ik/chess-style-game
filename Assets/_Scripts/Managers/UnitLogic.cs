using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;

public class UnitLogic
{
    //Делаю так, чтобы разбить зависимость singleton-ов, для того чтобы код можно было потестить по частям.
    private IGridManager gridManager;
    private IGameManager gameManager;
    private IMenuManager menuManager;
      
    private UnitsHolder unitsHolder;
    private IUnitFactory unitFactory;
        
    public UnitLogic(IGridManager gridManager, IGameManager gameManager, IMenuManager menuManager, IUnitFactory unitFactory)
    {
        this.gridManager = gridManager;
        this.gameManager = gameManager;
        this.menuManager = menuManager;
        this.unitFactory = unitFactory;
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

    public async Task MoveUnits(Faction faction)
    {
        IEnumerable<BaseUnit> unitsEnumerator = unitsHolder.GetUnits(faction);
        foreach (BaseUnit unit in unitsEnumerator)
        {
            List<UnitMove> validUnitMove = SequenceValidator.GetValidRandomUnitMove(unit.getMoveSequences(), unit.OccupiedTile, unit.getFaction());
            Debug.Log($"moved units {faction} {unit.getName()}");
            foreach (UnitMove unitMove in validUnitMove)
            {
                await Task.Delay(750);
                Debug.Log($"{faction} {unitMove.step.y}");
                bool doNextMovement = await TryMoveOrFight(faction, unit, unitMove);
                if (!doNextMovement || GameManager.Instance.IsGameEnded()) break;
            }
        }
        unitsHolder.compact();
    }

    //TODO: эту ф-ию явно можно упростить. Разбить на ф-ии попроще и часть унести в поведение юнита.
    private async Task<bool> TryMoveOrFight(Faction faction, BaseUnit unit, UnitMove unitMove)
    {  
        Tile occupiedTile = unit.OccupiedTile;
        Tile tileMoveTo = unitMove.validTileToMove;
        //Определяем что делать, в зависимости от того что на следующем tile
        if (unitMove.isAttackHeroMainHealth || unitMove.isAttackOpponentMainHealth)
        {
            await menuManager.DoDamageToMainHero(unit.getFaction(), unit.GetAttack());
            DestroyUnit(unit);
            occupiedTile.OccupiedUnit = null;
            return false;
        }
        else
        {
            if (tileMoveTo.OccupiedUnit != null) //Что делать, если кто-то уже есть на этом тайле
            {  
                //Это чужой юнит, нужно с ним сражаться
                return await Fight(unit, tileMoveTo.OccupiedUnit); //Юнит файтится 1 раз. Если пофайтился, дальше не двигается
            }
            else
            { //Клетка пустая, сдвигаемся
                tileMoveTo.SetUnit(unit);
                return true;
            }
        }
    }

    private async Task<bool> Fight(BaseUnit attackingUnit, BaseUnit defendingUnit)
    {
        float remainingHealth = await defendingUnit.getHealth().RecieveDamage(attackingUnit.GetAttack());

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
        unit.getUnityObject().Destroy();
    }
}
