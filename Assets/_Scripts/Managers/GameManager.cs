using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    public static GameManager Instance;
    public GameState GameState;

    void Awake()
    {
        Instance = this;
        Debug.Log("GameManager awaked");
    }

    void Start()
    {   
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;
        Debug.Log($"state {newState}");
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnEnemies:
                /*
                 * Должны появляться враги на последней линии
                 */
                UnitManager.Instance.SpawnEnemies();
                break;
            case GameState.EnemiesTurn:
                /*
                 * Сдвигаются юниты врага
                 * Если дошли до конца, наносят нам урон
                 */
                UnitManager.Instance.MoveUnitsAsync(Faction.Enemy);
                ChangeState(GameState.SpawnHeroes);
                break;
            case GameState.SpawnHeroes:
                /* 
                 * Должны выбрать юнита и поставить его на какую-то клетку.
                 * На первой линии
                 */
                //Стейт переключается в Tile, т.к нужно реагировать на нажатие мыши
                
                break;
            case GameState.HeroesTurn:
                /*
                 * Сдвигаются все наши юниты
                 * Если дошли до конца, они наносят урон 
                 */
                UnitManager.Instance.MoveUnitsAsync(Faction.Hero);
                ChangeState(GameState.SpawnEnemies);
                break;
            case GameState.GameEnd:
                /* 
                 * Когда закончилось ХП у кого-то, игра заканчивается
                 */
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

    }
}

public enum GameState
{
    GenerateGrid = 0,
    SpawnHeroes = 1,
    SpawnEnemies = 2,
    HeroesTurn = 3,
    EnemiesTurn = 4,
    GameEnd = 5
}
