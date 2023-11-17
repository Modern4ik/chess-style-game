using System.Collections.Generic;
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
            List<UnitMove> validUnitMove = SequenceValidator.GetValidRandomUnitMoves(unit.getMoveSequences(), unit.OccupiedTile, unit.getFaction());
            Debug.Log($"moved units {faction} {unit.getName()}");
            foreach (UnitMove unitMove in validUnitMove)
            {
                await Task.Delay(750);
                await ApplyUnitAction(unit, unitMove);
                if (GameManager.Instance.IsGameEnded()) break;
            }
        }
        unitsHolder.compact();
    }

    private async Task ApplyUnitAction(BaseUnit unit, UnitMove unitMove)
    {
        switch (unitMove)
        {
            case MoveTo: 
                await Move(unit, (MoveTo)unitMove);
                break;
            case AttackUnit:
                await Fight(unit, unitMove.validTileToMove.OccupiedUnit);
                break;
            case AttackMain: 
                await AttackMainSide(unit);
                break;
        }
    }

    private async Task Move(BaseUnit unit, MoveTo unitAction)
    {  
        Debug.Log($"{unit.getFaction()} moved {unitAction.validTileToMove.y}");

        await UnitView.Instance.StartMoveAnimation(unit, unitAction.validTileToMove.x, unitAction.validTileToMove.y);
        unitAction.validTileToMove.SetUnit(unit); 
    }

    private async Task AttackMainSide(BaseUnit unit)
    {
        await menuManager.DoDamageToMainHero(unit.getFaction(), unit.GetAttack());
        DestroyUnit(unit);
        unit.OccupiedTile.OccupiedUnit = null;
    }

    private async Task Fight(BaseUnit attackingUnit, BaseUnit defendingUnit)
    {
        await UnitView.Instance.StartFightAnimation(attackingUnit, defendingUnit);
        float remainingHealth = await defendingUnit.getHealth().RecieveDamage(attackingUnit.GetAttack());

        if (remainingHealth <= 0)
        {
            DestroyUnit(defendingUnit);
            await UnitView.Instance.StartMoveAnimation(attackingUnit, defendingUnit.OccupiedTile.x, defendingUnit.OccupiedTile.y);
            defendingUnit.OccupiedTile.SetUnit(attackingUnit);
        }
    }

    private void DestroyUnit(BaseUnit unit)
    {
        unitsHolder.DeleteUnit(unit);
        unit.getUnityObject().Destroy();
    }
}
