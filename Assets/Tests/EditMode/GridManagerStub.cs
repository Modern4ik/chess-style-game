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
        gameObject.AddComponent<Tile>();
        return gameObject.GetComponent<Tile>();
    }
    //TODO: Данный метод временно мокнут для тестов.
    public Tile GetTileAtPosition(Vector2 pos)
    {
        GameObject testTile = new GameObject("TestTile", typeof(Tile));
        var tileComp = testTile.GetComponent<Tile>();
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

    public Tile GetEnemySpawnTile()
    {
        throw new System.NotImplementedException();
    }
}
