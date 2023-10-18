using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class UnitManager : MonoBehaviour {
    public static UnitManager Instance;
    
    private UnitLogic unitLogic;
    
    void Awake() {
        Instance = this;
        unitLogic = new UnitLogic(GridManager.Instance, GameManager.Instance, MenuManager.Instance, 
            GridManager.Instance.GetGridWidth(), GridManager.Instance.GetGridHeight());
    }

    public void SpawnHero(Tile tile)
    {
        unitLogic.SpawnHero(tile);
    }

    public void SpawnEnemies()
    {
        unitLogic.SpawnEnemies();
    }

    public async void MoveUnitsAsync(Faction faction)
    {
        Debug.Log($"move units {faction}");
        await Task.Delay(500);
        unitLogic.MoveUnits(faction);
    }
}
