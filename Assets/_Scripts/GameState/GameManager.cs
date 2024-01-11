using System;
using UnityEngine;
using UserInput;
using GameLogic;
using GameLogic.Units;
using GameLogic.Factory;
using GameSettings;

namespace GameState
{
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
                    GameMenuManager.Instance.GenerateUnitSelectMenu();

                    ChangeState(GameState.SpawnEnemies);
                    break;
                case GameState.SpawnEnemies:
                    await GameMenuManager.Instance.GenerateTurnNotification(Faction.Enemy);

                    unitLogic.SpawnEnemies();

                    ChangeState(GameState.EnemiesTurn);
                    break;
                case GameState.EnemiesTurn:
                    await unitLogic.MoveUnits(Faction.Enemy);

                    if (GameStatus.isPlayerDead || GameStatus.isOpponentDead) ChangeState(GameState.GameEnded);
                    else ChangeState(GameState.SpawnHeroes);

                    break;
                case GameState.SpawnHeroes:
                    GameMenuManager.Instance.GenerateTurnNotification(Faction.Hero);

                    GameStatus.isAwaitPlayerInput = true;
                    InputData inputData = await playerInput.SelectUnitToResp();

                    unitLogic.SpawnHero(inputData);
                    TileInput.tileDroppedOn = null;

                    // Временное решение для демонстрации анимации PsyEnergyBar.
                    HeroManager.Instance.playerHeroView.SpendPsyEnergy(1);

                    ChangeState(GameState.HeroesTurn);
                    break;
                case GameState.HeroesTurn:
                    GameMenuManager.Instance.UpdateUnitSelectMenu();

                    await unitLogic.MoveUnits(Faction.Hero);

                    if (GameStatus.isPlayerDead || GameStatus.isOpponentDead) ChangeState(GameState.GameEnded);
                    else ChangeState(GameState.SpawnEnemies);

                    break;
                case GameState.GameEnded:
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
}