using System;
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

    public async void ChangeState(GameState newState)
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
                await MenuManager.Instance.GenerateTurnNotification();

                UnitManager.Instance.SpawnEnemies();
                break;
            case GameState.EnemiesTurn:
                /*
                 * Сдвигаются юниты врага
                 * Если дошли до конца, наносят нам урон
                 */
                await UnitManager.Instance.MoveUnitsAsync(Faction.Enemy);

                if (HeroManager.Instance.isPlayerDead || HeroManager.Instance.isOpponentDead) ChangeState(GameState.GameEnded);
                else ChangeState(GameState.SpawnHeroes);

                break;
            case GameState.SpawnHeroes:
                /* 
                 * Должны выбрать юнита и поставить его на какую-то клетку.
                 * На первой линии
                 */
                //Стейт переключается в Tile, т.к нужно реагировать на нажатие мыши
                await MenuManager.Instance.GenerateTurnNotification();
                break;
            case GameState.HeroesTurn:
                /*
                 * Сдвигаются все наши юниты
                 * Если дошли до конца, они наносят урон 
                 */
                MenuManager.Instance.UpdateUnitSelectMenu();

                await UnitManager.Instance.MoveUnitsAsync(Faction.Hero);

                if (HeroManager.Instance.isPlayerDead || HeroManager.Instance.isOpponentDead) ChangeState(GameState.GameEnded);
                else ChangeState(GameState.SpawnEnemies);

                break;
            case GameState.GameEnded:
                /* 
                 * Когда закончилось ХП у кого-то, игра заканчивается
                 */
                if (HeroManager.Instance.isOpponentDead) MenuManager.Instance.GenerateWinMenu();
                else MenuManager.Instance.GenerateLoseMenu();

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
    GameEnded = 5
}
