using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour, IGridManager {
    public static GridManager Instance;

    [SerializeField] private TileView _grassTile, _mountainTile;

    [SerializeField] private Transform _cam;

    [SerializeField] private GameObject _tileObject;
    [SerializeField] private GameObject _tileUnitObject;

    private int _width = GridSettings.WIDTH;
    private int _height = GridSettings.HEIGHT;

    private Dictionary<Vector2, GameTile> _tiles;

    void Awake() {
        Instance = this;
        Debug.Log("GridManager awaked");
    }

    public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, GameTile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++) {
                //var randomTile = Random.Range(0, 6) == 3 ? _mountainTile : _grassTile;
                TileView spawnedTileView = Instantiate(_grassTile, new Vector3(x, y), Quaternion.identity);
                GameTile spawnedTile = new GameTile(x, y, spawnedTileView);

                spawnedTileView.name = $"Tile {x} {y}";
                spawnedTileView.Init(_tileObject, _tileUnitObject);
                spawnedTileView.transform.GetComponent<TileInput>().Init(spawnedTile);

                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    }

    public GameTile GetHeroSpawnTile() {
        return _tiles.Where(t => t.Key.x < _width / 2 && t.Value.walkable).OrderBy(t => Random.value).First().Value;
    }

    public GameTile GetEnemySpawnTile()
    {
        return _tiles.Where(t => t.Key.y == _width - 1 && t.Value.walkable).OrderBy(t => Random.value).First().Value;
    }

    public GameTile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}