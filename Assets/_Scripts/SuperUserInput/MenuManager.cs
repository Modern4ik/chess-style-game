using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using GameLogic.Factory;
using GameLogic.Units;
using GameSettings;
using View.UI;

namespace SuperUserInput
{
    public class MenuManager : MonoBehaviour, IMenuManager
    {
        public static MenuManager Instance;

        private GameObject unitMenu;
        private GameObject pauseMenu;

        [SerializeField] private GameObject _selectedHeroObject;
        [SerializeField] private GameObject _unitSelectMenu;
        [SerializeField] private GameObject _endGameMenuPrefab;
        [SerializeField] private GameObject _pauseMenuPrefab;
        [SerializeField] private GameObject _winEndMenuText, _loseEndMenuText;
        [SerializeField] private GameObject _turnNotificationPrefab;
        // Поле ниже подтягивает Canvas(именно Transform Канваса) из иерархии в Unity.
        // Оно нужно для того, чтобы разместить healthBar ввиде дочернего объекта в Canvas по иерархии на сцене.
        [SerializeField] private Transform _canvas;

        void Awake()
        {
            Instance = this;

            GenerateUnitSelectMenu();
            GameStatus.isGameActive = true;
            Debug.Log("MenuManager awaked");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!GameStatus.isGameActive) ResumeGame();
                else PauseGame();
            }
        }

        public void GenerateUnitSelectMenu()
        {
            unitMenu = Instantiate(_unitSelectMenu, _canvas.transform);
            UpdateUnitSelectMenu();
        }

        public void UpdateUnitSelectMenu()
        {
            for (int i = 1; i <= unitMenu.transform.childCount; i++)
            {
                GameObject.Find($"UnitBlank{i}").GetComponent<Image>().color = PrefabSettingsChanger.SetRandomColor();
            }
        }

        public void GenerateWinMenu()
        {
            GameObject winMenu = Instantiate(_endGameMenuPrefab, _canvas.transform);
            Instantiate(_winEndMenuText, winMenu.transform);

            GameStatus.isGameActive = false;
        }

        public void GenerateLoseMenu()
        {
            GameObject loseMenu = Instantiate(_endGameMenuPrefab, _canvas.transform);
            Instantiate(_loseEndMenuText, loseMenu.transform);

            GameStatus.isGameActive = false;
        }

        public async Task GenerateTurnNotification(Faction faction)
        {
            await Task.Delay(1500);

            GameObject turnNotification = Instantiate(_turnNotificationPrefab, _canvas.transform);
            turnNotification.transform.SetAsFirstSibling();
            await NotificationView.Instance.StartNotificationAnimation(faction);

            Destroy(turnNotification);
        }

        private void ResumeGame()
        {
            Destroy(pauseMenu);
            Time.timeScale = 1f;

            GameStatus.isGameActive = true;
        }

        private void PauseGame()
        {
            pauseMenu = Instantiate(_pauseMenuPrefab, _canvas.transform);
            Time.timeScale = 0f;

            GameStatus.isGameActive = false;
        }
    }
}