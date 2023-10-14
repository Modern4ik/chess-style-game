using UnityEngine;

public class GridManagerStub : IGridManager
{
    private static int size = 8;
    private Tile[,] grid = new Tile[size, size];
    
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

    private Tile CreateTile()
    {
        GameObject gameObject = new GameObject();
        gameObject.AddComponent<TestTile>();
        return gameObject.GetComponent<TestTile>();
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        throw new System.NotImplementedException();
    }

    public Tile GetEnemySpawnTile()
    {
        throw new System.NotImplementedException();
    }
}
