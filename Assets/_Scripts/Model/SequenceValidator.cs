using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SequenceValidator
{
    public static List<UnitMove> GetValidRandomUnitMoves(List<List<Coordinate>> moveSequences, Tile occupiedTile, Faction faction)
    {
        List<List<UnitMove>> validUnitMoves = new List<List<UnitMove>>();

        foreach (List<Coordinate> sequence in moveSequences)
        {
            List<UnitMove> unitMoves = GetValidUnitMoves(sequence, occupiedTile, faction);

            if (unitMoves.Count > 0) validUnitMoves.Add(unitMoves);
        }
        
        return GetRandomMoves(validUnitMoves);
    }

    public static List<UnitMove> GetValidUnitMoves(List<Coordinate> sequence, Tile startTile, Faction faction)
    {
        Tile currentTile = startTile;
        List<UnitMove> unitMoves = new List<UnitMove>();

        foreach (Coordinate coord in sequence)
        {
            int moveToX = currentTile.x + coord.x;
            int moveToY = currentTile.y + coord.y;
            Tile tileMoveTo = GridManager.Instance.GetTileAtPosition(new Vector2(moveToX, moveToY));

            if (tileMoveTo != null && IsAllyOnTile(tileMoveTo, faction))
            {
                unitMoves.Add(new MoveTo(coord, tileMoveTo));

                if (IsEnemyOnTile(tileMoveTo, faction)) break;

                currentTile = tileMoveTo;
            }
            else if (isAttackMainSideBorders(moveToY))
            {
                unitMoves.Add(new AttackMain(moveToY));
                break;
            }
        }
        return unitMoves;
    }

    private static List<UnitMove> GetRandomMoves(List<List<UnitMove>> validUnitMoves)
    {
        if (validUnitMoves.Count > 0) return validUnitMoves.OrderBy(o => Random.value).First();

        else return new List<UnitMove>();
    }

    private static bool IsAllyOnTile(Tile tileMoveTo, Faction faction)
    {
        if (tileMoveTo.OccupiedUnit != null && tileMoveTo.OccupiedUnit.getFaction() == faction)
        {
            return false;
        }

        return true;
    }

    private static bool IsEnemyOnTile(Tile tileMoveTo, Faction faction)
    {
        if (tileMoveTo.OccupiedUnit != null && tileMoveTo.OccupiedUnit.getFaction() != faction)
        {
            return true;
        }

        return false;
    }

    private static bool isAttackMainSideBorders(int coordY) => coordY < 0 || coordY > GridSettings.HEIGHT - 1;
    
}