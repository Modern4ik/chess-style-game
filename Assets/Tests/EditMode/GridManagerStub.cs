using UnityEngine;
using View;

namespace GameLogic
{
    public class GridManagerStub : IGridManager
    {
        private static int size = 8;
        private TileView[,] grid = new TileView[size, size];

        public void GenerateGrid()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = CreateTile();
                }
            }
        }

        private TileView CreateTile()
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<TileView>();
            return gameObject.GetComponent<TileView>();
        }
        //TODO: Данный метод временно мокнут для тестов.
        public GameTile GetTileAtPosition(Vector2 pos)
        {
            GameObject testTile = new GameObject("TestTile", typeof(GameTile));
            var tileComp = testTile.GetComponent<GameTile>();
            switch (pos.y)
            {
                case 1:
                    tileComp.y = 1;
                    break;
                case 6:
                    tileComp.y = 6;
                    break;
                case 7:
                    tileComp.y = 7;
                    break;
            }

            return tileComp;
        }

        public GameTile GetEnemySpawnTile()
        {
            throw new System.NotImplementedException();
        }
    }
}