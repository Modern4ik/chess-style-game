using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour, IMenuManager {
    public static MenuManager Instance;
    private static int playerMaxHealth = 10;
    private Health playerHealth = new Health(playerMaxHealth);
    private Health enemyHealth = new Health(10);

    [SerializeField] private GameObject _selectedHeroObject,_tileObject,_tileUnitObject;
    [SerializeField] private Image _healthbarSprite;

    public void DamagePlayer(int damage)
    {
        int currentHealth = playerHealth.GetDamage(damage);
        UpdateHealthBar(playerMaxHealth, currentHealth);
    }

    private void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _healthbarSprite.fillAmount = currentHealth / maxHealth;
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
