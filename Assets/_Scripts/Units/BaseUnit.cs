using UnityEngine;
using UnityEngine.UI;

public abstract class BaseUnit
{
    private string name;
    private Faction faction;
    private Health health;
    private int atack;
    private MovePattern movePattern;
    private MonoBehaviour monoBehaviour;
    private Image healthBar;
    
    protected BaseUnit(string name, Faction faction, Health health, int atack, MovePattern movePattern, MonoBehaviour monoBehaviour)
    {
        this.name = name;
        this.faction = faction;
        this.health = health;
        this.atack = atack;
        this.movePattern = movePattern;
        this.monoBehaviour = monoBehaviour;
        this.healthBar = monoBehaviour.transform.Find("UnitCanvas/HealthBar/Foreground").
            GetComponent<Image>();
    }
    public string getName()
    {
        return name;
    }

    public Faction getFaction()
    {
        return faction;
    }

    public MovePattern getMovePattern()
    {
        return movePattern;
    }

    public Health getHealth()
    {
        return health;
    }

    public int getAtack()
    {
        return atack;
    }

    public Health receiveDamage(int damage)
    {
        health.GetDamage(damage);
        UpdateUnitHealthBar();

        return health;
    }

    protected void UpdateUnitHealthBar()
    {
        healthBar.fillAmount = (float)health.GetCurrentHealth() / health.GetMaxHealth();
    }

    public MonoBehaviour getUnityObject()
    {
        return monoBehaviour;
    }
    
    public Tile OccupiedTile { get; set; }
}
