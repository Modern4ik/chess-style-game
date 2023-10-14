using UnityEngine;

public interface IGridManager
{
    public void GenerateGrid();
    public Tile GetTileAtPosition(Vector2 pos);
    public Tile GetEnemySpawnTile();
}