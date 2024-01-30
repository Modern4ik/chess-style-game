using UnityEngine;
using GameSettings;
using GameState;

namespace SuperUserInput
{
    public class SystemMenuManager : MonoBehaviour, ISystemMenuManager
    {
        private GameObject pauseMenu;
        
        [SerializeField] private GameObject _selectedHeroObject;
        [SerializeField] private GameObject _endGameMenuPrefab;
        [SerializeField] private GameObject _pauseMenuPrefab;
        [SerializeField] private GameObject _winEndMenuText, _loseEndMenuText;
        // Поле ниже подтягивает Canvas(именно Transform Канваса) из иерархии в Unity.
        // Оно нужно для того, чтобы разместить healthBar ввиде дочернего объекта в Canvas по иерархии на сцене.
        [SerializeField] private Transform _generalCanvasTransform;

        void Awake()
        {
            GameStatus.isGameActive = true;
            GameStatus.isPlayerDead = false;
            GameStatus.isOpponentDead = false;

            Debug.Log("MenuManager awaked");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!GameStatus.isGameActive) ResumeGame();
                else PauseGame();
            }
            else if ((GameStatus.isPlayerDead || GameStatus.isOpponentDead) && GameStatus.isGameActive)
            {
                GenerateEndGameMenu();
                GameStatus.isGameActive = false;
            }
        }

        private void GenerateEndGameMenu()
        {
            if (GameStatus.isPlayerDead) GenerateEndMenu(_loseEndMenuText);
            else GenerateEndMenu(_winEndMenuText);

        }

        private void GenerateEndMenu(GameObject menuText)
        {
            GameObject endMenu = Instantiate(_endGameMenuPrefab, _generalCanvasTransform);
            Instantiate(menuText, endMenu.transform);
        }

        private void ResumeGame()
        {
            Destroy(pauseMenu);
            Time.timeScale = 1f;

            GameStatus.isGameActive = true;
        }

        private void PauseGame()
        {
            pauseMenu = Instantiate(_pauseMenuPrefab, _generalCanvasTransform);
            Time.timeScale = 0f;

            GameStatus.isGameActive = false;
        }
    }
}