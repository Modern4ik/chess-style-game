using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseUnit
{
    private string name;
    private Faction faction;
    private IHealth health;
    private int atack;
    private MovePattern movePattern;
    private MonoBehaviour monoBehaviour;
    
    protected BaseUnit(string name, Faction faction, int maxHealth, int atack, MovePattern movePattern, MonoBehaviour monoBehaviour)
    {   
        switch (faction)
        {
            case Faction.Enemy:
                this.movePattern = new MovePattern(movePattern.moveSequences.Select(steps => steps.Select(coord => new Coordinate(coord.x, -coord.y)).ToList()).ToList());
                break;
            case Faction.Hero:
                this.movePattern = movePattern;
                break;
            default:
                throw new System.Exception($"Unexpected faction: {faction}");
        }
        this.name = name;
        this.atack = atack;
        this.monoBehaviour = monoBehaviour;
        
        this.faction = faction;
        Image healthBar = monoBehaviour.transform.Find("UnitCanvas/HealthBar/Foreground").
            GetComponent<Image>();
        this.health = new Health(maxHealth, new HealthView(healthBar));
    }
    public string getName()
    {
        return name;
    }

    public Faction getFaction()
    {
        return faction;
    }

    public List<List<Coordinate>> getMoveSequences()
    {
        return movePattern.moveSequences;
    }

    public IHealth getHealth()
    {
        return health;
    }

    public int getAtack()
    {
        return atack;
    }

    public MonoBehaviour getUnityObject()
    {
        return monoBehaviour;
    }
    
    public Tile OccupiedTile { get; set; }
}
