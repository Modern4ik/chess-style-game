using UnityEngine;

namespace GameLogic
{
    public interface IGridManager
    {
        public void GenerateGrid();
        public GameTile GetTileAtPosition(Vector2 pos);
        public GameTile GetEnemySpawnTile();
    }
}
