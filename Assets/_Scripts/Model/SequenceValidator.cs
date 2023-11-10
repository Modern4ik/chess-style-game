using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SequenceValidator
{
    private static List<Tile> validTiles = new List<Tile>();
    public static List<Coordinate> GetValidSequence(List<List<Coordinate>> moveSequences, Tile occupiedTile, Faction faction)
    {
        List<List<Coordinate>> validSequences = new List<List<Coordinate>>();

        foreach (List<Coordinate> sequence in moveSequences)
        {
            List<Coordinate> steps = GetValidSteps(sequence, occupiedTile, faction);

            if (steps.Count > 0) validSequences.Add(steps);
        }

        return GetRandomSequence(validSequences);
    }

    public static List<Tile> GetValidTiles(BaseUnit unitOnTile)
    {
        if (validTiles.Count > 0) validTiles.Clear();

        GetValidSequence(unitOnTile.getMoveSequences(), unitOnTile.OccupiedTile, unitOnTile.getFaction());
        return validTiles;
    }

    private static List<Coordinate> GetValidSteps(List<Coordinate> sequence, Tile startTile, Faction faction)
    {
        Tile currentTile = startTile;
        List<Coordinate> validSteps = new List<Coordinate>();

        foreach (Coordinate coord in sequence)
        {
            int moveToX = currentTile.x + coord.x;
            int moveToY = currentTile.y + coord.y;
            Tile tileMoveTo = GridManager.Instance.GetTileAtPosition(new Vector2(moveToX, moveToY));

            if (CheckSideBorders(moveToX) && IsAllyOnTile(tileMoveTo, faction))
            {
                validSteps.Add(coord);

                if (IsEnemyOnTile(tileMoveTo, faction))
                {
                    validTiles.Add(tileMoveTo);
                    break;
                }
            }
            else break;

            if (tileMoveTo != null)
            {
                validTiles.Add(tileMoveTo);
                currentTile = tileMoveTo;
            }
        }
        return validSteps;
    }

    private static List<Coordinate> GetRandomSequence(List<List<Coordinate>> moveSequences)
    {
        if (moveSequences.Count > 0) return moveSequences.OrderBy(o => Random.value).First();

        else return new List<Coordinate>();
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