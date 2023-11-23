using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour, IMenuManager {
    public static MenuManager Instance;
    public bool isGamePaused = false;

    private GameObject unitMenu;
    public MainHeroView playerHero;
    public MainHeroView opponentHero;
    private GameObject pauseMenu;
    
    [SerializeField] private GameObject _selectedHeroObject,_tileObject,_tileUnitObject;
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
        Debug.Log("MenuManager awaked");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused) ResumeGame();
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

    public void GenerateEndGameMenu(string mainHeroViewTag)
    {   
        switch (mainHeroViewTag)
        {
            case "Player":
                GameObject loseMenu = Instantiate(_endGameMenuPrefab, _canvas.transform);
                Instantiate(_loseEndMenuText, loseMenu.transform);

                break;

            case "Opponent":
                GameObject winMenu = Instantiate(_endGameMenuPrefab, _canvas.transform);
                Instantiate(_winEndMenuText, winMenu.transform);

                break;
        }

        GameManager.Instance.ChangeState(GameState.GameEnded);
    }

    public async Task GenerateTurnNotification()
    {
        await Task.Delay(1500);

        GameObject turnNotification = Instantiate(_turnNotificationPrefab, _canvas.transform);
        turnNotification.transform.SetAsFirstSibling();
        await NotificationView.Instance.StartNotificationAnimation();

        Destroy(turnNotification);    
    }

    private void ResumeGame()
    {
        Destroy(pauseMenu);
        Time.timeScale = 1f;

        isGamePaused = false;
    }

    private void PauseGame()
    {
        pauseMenu = Instantiate(_pauseMenuPrefab, _canvas.transform);
        Time.timeScale = 0f;

        isGamePaused = true;
    }

    public void ShowTileInfo(Tile tile) {

        if (tile == null)
        {
            _tileObject.SetActive(false);
            _tileUnitObject.SetActive(false);
            return;
        }

        _tileObject.GetComponentInChildren<Text>().text = tile.TileName;
        _tileObject.SetActive(true);

        if (tile.OccupiedUnit != null) {
            _tileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.getName();
            _tileUnitObject.SetActive(true);
        }
    }
}
