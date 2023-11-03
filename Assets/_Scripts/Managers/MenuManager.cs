﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour, IMenuManager {
    public static MenuManager Instance;

    private IHealth playerHealth;
    private IHealth enemyHealth;
    
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

    public void DoDamageToMainHero(Faction unitFaction)
    {   
        switch (unitFaction)
        {
            case Faction.Hero:
                if (enemyHealth.RecieveDamage(1) == 0) GenerateEndGameMenu(_winEndMenuText);
                break;
            case Faction.Enemy:
                if (playerHealth.RecieveDamage(1) == 0) GenerateEndGameMenu(_loseEndMenuText);
                break;
        }
    }

    public void GenerateHealthBars()
    {
        Image playerHealthSprite = GenerateHealthBar(_alliedHealthPrefab);
        Image enemyHealthSprite = GenerateHealthBar(_enemyHealthPrefab);

        this.playerHealth = new Health(10, new HealthView(playerHealthSprite));
        this.enemyHealth = new Health(10, new HealthView(enemyHealthSprite));
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

    private void GenerateEndGameMenu(GameObject menuText)
    {
        GameObject endMenu = Instantiate(_endGameMenuPrefab, _canvas.transform);
        Instantiate(menuText, endMenu.transform);

        GameManager.Instance.ChangeState(GameState.GameEnd);
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
