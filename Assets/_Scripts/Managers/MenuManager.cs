using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour, IMenuManager {
    public static MenuManager Instance;
    private static int playerMaxHealth = 10;
    private static int enemyMaxHealth = 10;
    private Health playerHealth = new Health(playerMaxHealth);
    private Health enemyHealth = new Health(enemyMaxHealth);
    private Image playerHealthSprite;
    private Image enemyHealthSprite;

    [SerializeField] private GameObject _selectedHeroObject,_tileObject,_tileUnitObject;
    [SerializeField] private GameObject _alliedHealthPrefab;
    [SerializeField] private GameObject _enemyHealthPrefab;
    // Поле ниже подтягивает Canvas(именно Transform Канваса) из иерархии в Unity.
    // Оно нужно для того, чтобы разместить healthBar ввиде дочернего объекта в Canvas по иерархии на сцене.
    [SerializeField] private Transform _canvas;

    public void DamagePlayer(int damage)
    {
        int currentHealth = playerHealth.GetDamage(damage);
        UpdateHealthBar(playerMaxHealth, currentHealth, Faction.Hero);
    }

    public void DamageEnemy(int damage)
    {
        int currentHealth = enemyHealth.GetDamage(damage);
        UpdateHealthBar(enemyMaxHealth, currentHealth, Faction.Enemy);
    }

    public void GenerateHealthBars()
    {
        GameObject playerHealthBar = Object.Instantiate(_alliedHealthPrefab, _canvas.transform);
        playerHealthBar.transform.localPosition = new Vector3(0, _canvas.GetComponent<RectTransform>().sizeDelta.y / -2, 0);
        playerHealthSprite = playerHealthBar.transform.GetChild(0).GetComponent<Image>();

        GameObject enemyHealthBar = Object.Instantiate(_enemyHealthPrefab, _canvas.transform);
        enemyHealthBar.transform.localPosition = new Vector3(0, _canvas.GetComponent<RectTransform>().sizeDelta.y / 2, 0);
        enemyHealthSprite = enemyHealthBar.transform.GetChild(0).GetComponent<Image>();
    }

    private void UpdateHealthBar(float maxHealth, float currentHealth, Faction faction)
    {   
        switch (faction)
        {
            case Faction.Enemy:
                enemyHealthSprite.fillAmount = currentHealth / maxHealth;
                break;
            case Faction.Hero:
                playerHealthSprite.fillAmount = currentHealth / maxHealth;
                break;
        }
    }


    void Awake() {
        Instance = this;
        Debug.Log("MenuManager awaked");
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
