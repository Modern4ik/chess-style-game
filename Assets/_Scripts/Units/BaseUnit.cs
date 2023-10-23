using UnityEngine;

public abstract class BaseUnit
{
    private string name;
    private Faction faction;
    private Health health;
    private MovePattern movePattern;
    private MonoBehaviour monoBehaviour;
    
    protected BaseUnit(string name, Faction faction, Health health, MovePattern movePattern, MonoBehaviour monoBehaviour)
    {
        this.name = name;
        this.faction = faction;
        this.health = health;
        this.movePattern = movePattern;
        this.monoBehaviour = monoBehaviour;
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

    public Health receiveDamage(int damage)
    {
        health.GetDamage(damage);
        return health;
    }

    public MonoBehaviour getUnityObject()
    {
        return monoBehaviour;
    }
    
    public Tile OccupiedTile { get; set; }
}
