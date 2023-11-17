using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        unitLogic = new UnitLogic(GridManager.Instance, GameManager.Instance, MenuManager.Instance, new UnitFactory(new UnitPrefabLoader()));
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
        await unitLogic.MoveUnits(faction);

        /*
         * Это временный(может и постоянный) костыль, чтобы игра останавливалась при появлении EndGameMenu,
         * иначе данный метод в асинхронее продолжает менять стейты.
         */
        if (GameManager.Instance.IsGameEnded()) return;

        switch (faction)
        {
            case Faction.Hero:
                GameManager.Instance.ChangeState(GameState.SpawnEnemies);
                break;
            case Faction.Enemy:
                await MenuManager.Instance.GenerateTurnNotification();

                GameManager.Instance.ChangeState(GameState.SpawnHeroes);
                break;
        }
    }
}
