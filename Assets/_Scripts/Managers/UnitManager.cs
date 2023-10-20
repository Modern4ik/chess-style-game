using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class UnitManager : MonoBehaviour {
    public static UnitManager Instance;

    //TODO: поле инициализируется на данный момент в GameManager
    public UnitLogic unitLogic;

    void Awake() {
        Instance = this;
        Debug.Log("UnitManager awaked");
        //TODO: для инициализации unitLogic отрабатывает настроенная фича Unity (Script Execution Order)
        // в ней настроен нужный порядок работы скриптов/инициализации
        unitLogic = new UnitLogic(GridManager.Instance, GameManager.Instance, MenuManager.Instance);
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
