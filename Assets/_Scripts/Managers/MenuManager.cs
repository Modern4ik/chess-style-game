using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour, IMenuManager {
    public static MenuManager Instance;

    private IHealth playerHealth;
    private IHealth enemyHealth;
    private GameObject heroAttackMark;
    private GameObject enemyAttackMark;
    private GameObject unitMenu;
    
    [SerializeField] private GameObject _selectedHeroObject,_tileObject,_tileUnitObject;
    [SerializeField] private GameObject _alliedHealthPrefab, _enemyHealthPrefab;
    [SerializeField] private GameObject _heroAttackMarkPrefab, _enemyAttackMarkPrefab;
    [SerializeField] private GameObject _unitSelectMenu;
    [SerializeField] private GameObject _endGameMenuPrefab;
    [SerializeField] private GameObject _winEndMenuText, _loseEndMenuText;
    [SerializeField] private GameObject _turnNotificationPrefab;
    // Поле ниже подтягивает Canvas(именно Transform Канваса) из иерархии в Unity.
    // Оно нужно для того, чтобы разместить healthBar ввиде дочернего объекта в Canvas по иерархии на сцене.
    [SerializeField] private Transform _canvas;

    void Awake()
    {
        Instance = this;

        GenerateHealthBars();
        GenerateUnitSelectMenu();
        heroAttackMark = GenerateAttackMark(_heroAttackMarkPrefab);
        enemyAttackMark = GenerateAttackMark(_enemyAttackMarkPrefab);

        Debug.Log("MenuManager awaked");
    }

    public async Task DoDamageToMainHero(Faction unitFaction, Attack unitAttack)
    {   
        switch (unitFaction)
        {
            case Faction.Hero:
                if (await enemyHealth.RecieveDamage(unitAttack) == 0) GenerateEndGameMenu(_winEndMenuText);
                break;
            case Faction.Enemy:
                if (await playerHealth.RecieveDamage(unitAttack) == 0) GenerateEndGameMenu(_loseEndMenuText);
                break;
        }
    }

    public void GenerateHealthBars()
    {
        HealthView playerHealthView = GenerateHealthBar(_alliedHealthPrefab);
        HealthView enemyHealthView = GenerateHealthBar(_enemyHealthPrefab);

        this.playerHealth = new Health(10, new Defense(1, 1, 1), playerHealthView);
        this.enemyHealth = new Health(10, new Defense(1, 1, 1), enemyHealthView);
    }

    private HealthView GenerateHealthBar(GameObject prefab)
    {
        GameObject sideHealthBar = Instantiate(prefab, _canvas.transform);

        return sideHealthBar.transform.GetComponent<HealthView>();
    }

    private GameObject GenerateAttackMark(GameObject markPrefab) => Instantiate(markPrefab, _canvas.transform);
   
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

    public void EnableHeroAttackMark(bool isEnable) => this.heroAttackMark.SetActive(isEnable);
    public void EnableEnemyAttackMark(bool isEnable) => this.enemyAttackMark.SetActive(isEnable);

    private void GenerateEndGameMenu(GameObject menuText)
    {
        GameObject endMenu = Instantiate(_endGameMenuPrefab, _canvas.transform);
        Instantiate(menuText, endMenu.transform);

        GameManager.Instance.ChangeState(GameState.GameEnded);
    }

    public async Task GenerateTurnNotification()
    {
        await Task.Delay(1500);

        GameObject turnNotification = Instantiate(_turnNotificationPrefab, _canvas.transform);
        await NotificationView.Instance.StartNotificationAnimation();

        Destroy(turnNotification);    
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
