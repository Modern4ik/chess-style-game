using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    [SerializeField] private GameObject _unitSelectMenu;
    // Поле ниже подтягивает Canvas(именно Transform Канваса) из иерархии в Unity.
    // Оно нужно для того, чтобы разместить healthBar ввиде дочернего объекта в Canvas по иерархии на сцене.
    [SerializeField] private Transform _canvas;

    void Awake()
    {
        Instance = this;
        Debug.Log("MenuManager awaked");
    }

    public void DoDamageToMainHero(Faction unitFaction)
    {
        float currentHealth;

        switch (unitFaction)
        {
            case Faction.Hero:
                currentHealth = enemyHealth.GetDamage(1);
                enemyHealthSprite.fillAmount = currentHealth / enemyMaxHealth;
                break;
            case Faction.Enemy:
                currentHealth = playerHealth.GetDamage(1);
                playerHealthSprite.fillAmount = currentHealth / playerMaxHealth;
                break;
        }
    }

    public void GenerateHealthBars()
    {
        playerHealthSprite = GenerateHealthBar(_alliedHealthPrefab);
        enemyHealthSprite = GenerateHealthBar(_enemyHealthPrefab);
    }

    private Image GenerateHealthBar(GameObject prefab)
    {
        GameObject sideHealthBar = Instantiate(prefab, _canvas.transform);
        
        return sideHealthBar.transform.GetChild(0).GetComponent<Image>();
    }

    public void GenerateUnitSelectMenu()
    {
        Instantiate(_unitSelectMenu, _canvas.transform);
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
