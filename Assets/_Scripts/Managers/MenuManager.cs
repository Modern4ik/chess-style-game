using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour, IMenuManager {
    public static MenuManager Instance;
    private static int playerMaxHealth = 10;
    private Health playerHealth = new Health(playerMaxHealth);
    private Health enemyHealth = new Health(10);
    private Image healthBarSprite;

    [SerializeField] private GameObject _selectedHeroObject,_tileObject,_tileUnitObject;
    [SerializeField] private GameObject _healthBarPrefab;
    [SerializeField] private Transform _canvas; // Данное поле нужно для правильного размещения HP бара в иерархии Unity

    public void DamagePlayer(int damage)
    {
        int currentHealth = playerHealth.GetDamage(damage);
        UpdateHealthBar(playerMaxHealth, currentHealth);
    }

    public void GenerateHealthBar()
    {   
        //TODO: в данном месте происходит генерация объекта в Unity и сразу жe
        // инициализируется переменная для работы с спрайтом HP бара.
        healthBarSprite = Object.Instantiate(_healthBarPrefab, _canvas.transform).transform
            .GetChild(0).GetComponent<Image>();
    }

    private void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthBarSprite.fillAmount = currentHealth / maxHealth;
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

        if (tile.OccupiedUnit) {
            _tileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName;
            _tileUnitObject.SetActive(true);
        }
    }

    public void ShowSelectedHero(BaseHero hero) {
        if (hero == null) {
            _selectedHeroObject.SetActive(false);
            return;
        }

        _selectedHeroObject.GetComponentInChildren<Text>().text = hero.UnitName;
        _selectedHeroObject.SetActive(true);
    }
}
