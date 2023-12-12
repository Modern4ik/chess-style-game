using System;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    public static GameManager Instance;
    public GameState GameState;

    private UnitLogic unitLogic;
    private PlayerInput playerInput;

    void Awake()
    {
        Instance = this;

        unitLogic = new UnitLogic(GridManager.Instance, new UnitFactory(new UnitPrefabLoader()));
        playerInput = new MouseInput();

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

                ChangeState(GameState.SpawnEnemies);
                break;
            case GameState.SpawnEnemies:
                await MenuManager.Instance.GenerateTurnNotification(Faction.Enemy);

                unitLogic.SpawnEnemies();

                ChangeState(GameState.EnemiesTurn);
                break;
            case GameState.EnemiesTurn:
                await unitLogic.MoveUnits(Faction.Enemy);

                if (HeroManager.Instance.isPlayerDead || HeroManager.Instance.isOpponentDead) ChangeState(GameState.GameEnded);
                else ChangeState(GameState.SpawnHeroes);

                break;
            case GameState.SpawnHeroes:
                MenuManager.Instance.GenerateTurnNotification(Faction.Hero);

                GameStatus.isAwaitPlayerInput = true;
                InputData inputData = await playerInput.SelectUnitToResp();

                unitLogic.SpawnHero(inputData);
                Tile.tileDroppedOn = null;

                ChangeState(GameState.HeroesTurn);
                break;
            case GameState.HeroesTurn:
                MenuManager.Instance.UpdateUnitSelectMenu();

                await unitLogic.MoveUnits(Faction.Hero);

                if (HeroManager.Instance.isPlayerDead || HeroManager.Instance.isOpponentDead) ChangeState(GameState.GameEnded);
                else ChangeState(GameState.SpawnEnemies);

                break;
            case GameState.GameEnded:
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
