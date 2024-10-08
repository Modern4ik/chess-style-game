using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameLogic.Units;
using GameLogic.UnitMoves;
using GameSettings;
using GameLogic.Heroes;

namespace GameLogic
{
    public static class SequenceValidator
    {
        public static List<UnitMove> GetValidRandomUnitMoves(List<List<Coordinate>> moveSequences, GameTile occupiedTile, Faction faction)
        {
            List<List<UnitMove>> validMovesTo = new List<List<UnitMove>>();
            List<List<UnitMove>> validAttacksUnit = new List<List<UnitMove>>();

            foreach (List<Coordinate> sequence in moveSequences)
            {
                List<UnitMove> unitMoves = GetValidUnitMoves(sequence, occupiedTile, faction);

                if (unitMoves.Find(move => move.GetType() == typeof(AttackUnit)) != null) validAttacksUnit.Add(unitMoves);

                else if (unitMoves.Count > 0) validMovesTo.Add(unitMoves);
            }

            if (validAttacksUnit.Count > 0) return GetRandomActions(validAttacksUnit);
            else return GetRandomActions(validMovesTo);
        }

        public static List<UnitMove> GetValidUnitMoves(List<Coordinate> sequence, GameTile startTile, Faction faction)
        {
            GameTile currentTile = startTile;
            List<UnitMove> unitMoves = new List<UnitMove>();

            foreach (Coordinate coord in sequence)
            {
                int moveToX = currentTile.x + coord.x;
                int moveToY = currentTile.y + coord.y;
                GameTile tileMoveTo = GridManager.Instance.GetTileAtPosition(new Vector2(moveToX, moveToY));

                if (tileMoveTo != null && !IsAllyOnTile(tileMoveTo, faction))
                {
                    if (IsEnemyOnTile(tileMoveTo, faction))
                    {
                        unitMoves.Add(new AttackUnit(coord, tileMoveTo));
                        break;
                    }
                    else if(tileMoveTo.fruitOnTile != null)
                    {
                        unitMoves.Add(new TakeFruit(coord, tileMoveTo));
                        break;
                    }
                    else unitMoves.Add(new MoveTo(coord, tileMoveTo));

                    currentTile = tileMoveTo;
                }
                else if (isAttackMainSideBorders(moveToY))
                {
                    unitMoves.Add(new AttackMain(GetMainSideToAttack(faction)));
                    break;
                }
            }
            return unitMoves;
        }

        private static List<UnitMove> GetRandomActions(List<List<UnitMove>> validUnitActions)
        {
            if (validUnitActions.Count > 0) return validUnitActions.OrderBy(o => Random.value).First();

            else return new List<UnitMove>();
        }

        private static bool IsAllyOnTile(GameTile tileMoveTo, Faction faction)
        {
            if (tileMoveTo.occupiedUnit != null && tileMoveTo.occupiedUnit.getFaction() == faction)
            {
                return true;
            }

            return false;
        }

        private static bool IsEnemyOnTile(GameTile tileMoveTo, Faction faction)
        {
            if (tileMoveTo.occupiedUnit != null && tileMoveTo.occupiedUnit.getFaction() != faction)
            {
                return true;
            }

            return false;
        }

        private static BaseHero GetMainSideToAttack(Faction unitFaction)
        {
            switch (unitFaction)
            {
                case Faction.Hero: return HeroManager.Instance.opponentHero;
                case Faction.Enemy: return HeroManager.Instance.playerHero;
                default: throw new System.Exception($"Unit faction out of range {unitFaction}");
            }
        }

        private static bool isAttackMainSideBorders(int coordY) => coordY < 0 || coordY > GridSettings.HEIGHT - 1;

    }
}