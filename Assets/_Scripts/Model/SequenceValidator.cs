using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SequenceValidator
{
    public static List<UnitMove> GetValidRandomUnitMove(List<List<Coordinate>> moveSequences, Tile occupiedTile, Faction faction)
    {
        List<List<UnitMove>> validUnitMoves = new List<List<UnitMove>>();

        foreach (List<Coordinate> sequence in moveSequences)
        {
            List<UnitMove> unitMove = GetValidUnitMove(sequence, occupiedTile, faction);

            if (unitMove.Count > 0) validUnitMoves.Add(unitMove);
        }
        
        return GetRandomMove(validUnitMoves);
    }

    public static List<UnitMove> GetValidUnitMove(List<Coordinate> sequence, Tile startTile, Faction faction)
    {
        Tile currentTile = startTile;
        List<UnitMove> unitMoves = new List<UnitMove>();

        foreach (Coordinate coord in sequence)
        {
            int moveToX = currentTile.x + coord.x;
            int moveToY = currentTile.y + coord.y;
            Tile tileMoveTo = GridManager.Instance.GetTileAtPosition(new Vector2(moveToX, moveToY));

            if (CheckSideBorders(moveToX) && IsAllyOnTile(tileMoveTo, faction))
            {
                unitMoves.Add(new UnitMove(coord));

                if (IsEnemyOnTile(tileMoveTo, faction))
                {
                    unitMoves.Last().validTileToMove = tileMoveTo;
                    break;
                }
            }
            else break;

            if (tileMoveTo != null)
            {
                unitMoves.Last().validTileToMove = tileMoveTo;
                currentTile = tileMoveTo;
            }
            else
            {
                if (moveToY > GridSettings.HEIGHT - 1) unitMoves.Last().isAttackOpponentMainHealth = true;
                else if (moveToY < 0) unitMoves.Last().isAttackHeroMainHealth = true;
            }
        }
        return unitMoves;
    }

    private static List<UnitMove> GetRandomMove(List<List<UnitMove>> validUnitMoves)
    {
        if (validUnitMoves.Count > 0) return validUnitMoves.OrderBy(o => Random.value).First();

        else return new List<UnitMove>();
    }

    private static bool IsAllyOnTile(Tile tileMoveTo, Faction faction)
    {
        if (tileMoveTo == null) return true;

        if (tileMoveTo.OccupiedUnit != null && tileMoveTo.OccupiedUnit.getFaction() == faction)
        {
            return false;
        }

        return true;
    }

    private static bool IsEnemyOnTile(Tile tileMoveTo, Faction faction)
    {
        if (tileMoveTo == null) return false;

        if (tileMoveTo.OccupiedUnit != null && tileMoveTo.OccupiedUnit.getFaction() != faction)
        {
            return true;
        }

        return false;
    }

    private static bool CheckSideBorders(int coordX)
    {
        if (coordX >= 0 && coordX < GridSettings.WIDTH) return true;

        else return false;
    }
}