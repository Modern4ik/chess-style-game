using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UserInput;
using GameLogic.Units;
using GameLogic.UnitMoves;
using GameLogic.Factory;
using GameLogic.Holders;
using GameSettings;

namespace GameLogic
{
    public class UnitLogic
    {
        //Делаю так, чтобы разбить зависимость singleton-ов, для того чтобы код можно было потестить по частям.
        private IGridManager gridManager;

        private UnitsHolder unitsHolder;
        private IUnitFactory unitFactory;

        public UnitLogic(IGridManager gridManager, IUnitFactory unitFactory)
        {
            this.gridManager = gridManager;
            this.unitFactory = unitFactory;
            unitsHolder = new UnitsHolderImpl();
        }

        public BaseUnit SpawnHero(InputData inputData)
        {
            var unit = SpawnUnit(Faction.Hero, inputData);
            return unit;
        }

        public void SpawnEnemies()
        {
            var enemyCount = 1;

            for (int i = 0; i < enemyCount; i++)
            {
                var randomSpawnTile = gridManager.GetEnemySpawnTile();
                SpawnUnit(Faction.Enemy, new InputData { tileToSpawn = randomSpawnTile });
            }
        }

        public BaseUnit SpawnUnit(Faction faction, InputData inputData)
        {
            var unit = unitFactory.createUnit(faction, inputData);
            inputData.tileToSpawn.SetUnit(unit);
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
                }
                if (GameStatus.isPlayerDead || GameStatus.isOpponentDead) break;
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
                    await Fight(unit, unitMove.validTileToMove.occupiedUnit);
                    break;
                case AttackMain:
                    await AttackMainSide(unit, (AttackMain)unitMove);
                    break;
            }
        }

        private async Task Move(BaseUnit unit, MoveTo unitAction)
        {
            Debug.Log($"{unit.getFaction()} moved {unitAction.validTileToMove.y}");

            await unit.getUnitView().MoveAnimation(unitAction.validTileToMove.x, unitAction.validTileToMove.y);
            unitAction.validTileToMove.SetUnit(unit);
        }

        private async Task AttackMainSide(BaseUnit unit, AttackMain unitAction)
        {
            await unitAction.mainHeroToAttack.GetDamage(unit.GetAttack());
            DestroyUnit(unit);
            unit.OccupiedTile.occupiedUnit = null;
        }

        private async Task Fight(BaseUnit attackingUnit, BaseUnit defendingUnit)
        {
            await attackingUnit.getUnitView().FightAnimation(defendingUnit);
            float remainingHealth = await defendingUnit.getHealth().RecieveDamage(attackingUnit.GetAttack());

            if (remainingHealth <= 0)
            {
                DestroyUnit(defendingUnit);
                await attackingUnit.getUnitView().MoveAnimation(defendingUnit.OccupiedTile.x, defendingUnit.OccupiedTile.y);
                defendingUnit.OccupiedTile.SetUnit(attackingUnit);
            }
        }

        private void DestroyUnit(BaseUnit unit)
        {
            unitsHolder.DeleteUnit(unit);
            unit.getUnitView().Destroy();
        }
    }
}