using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour, IMenuManager {
    public static MenuManager Instance;

    private IHealth playerHealth;
    private IHealth enemyHealth;
    private GameObject unitMenu;
    
    [SerializeField] private GameObject _selectedHeroObject,_tileObject,_tileUnitObject;
    [SerializeField] private GameObject _alliedHealthPrefab;
    [SerializeField] private GameObject _enemyHealthPrefab;
    [SerializeField] private GameObject _unitSelectMenu;
    [SerializeField] private GameObject _endGameMenuPrefab;
    [SerializeField] private GameObject _winEndMenuText, _loseEndMenuText;
    // Поле ниже подтягивает Canvas(именно Transform Канваса) из иерархии в Unity.
    // Оно нужно для того, чтобы разместить healthBar ввиде дочернего объекта в Canvas по иерархии на сцене.
    [SerializeField] private Transform _canvas;

    void Awake()
    {
        Instance = this;
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
        HealthBar playerHealthBar = GenerateHealthBar(_alliedHealthPrefab);
        HealthBar enemyHealthBar = GenerateHealthBar(_enemyHealthPrefab);

        this.playerHealth = new Health(10, new Defense(1, 1, 1), new HealthView(playerHealthBar));
        this.enemyHealth = new Health(10, new Defense(1, 1, 1),new HealthView(enemyHealthBar));
    }

    private HealthBar GenerateHealthBar(GameObject prefab)
    {
        GameObject sideHealthBar = Instantiate(prefab, _canvas.transform);

        return sideHealthBar.transform.GetComponent<HealthBar>();
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

    private void GenerateEndGameMenu(GameObject menuText)
    {
        GameObject endMenu = Instantiate(_endGameMenuPrefab, _canvas.transform);
        Instantiate(menuText, endMenu.transform);

        GameManager.Instance.ChangeState(GameState.GameEnded);
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
