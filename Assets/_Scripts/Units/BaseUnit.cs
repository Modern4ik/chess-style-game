using System.Collections.Generic;
using System.Linq;

public abstract class BaseUnit
{
    private string name;
    private Faction faction;
    private Element unitElement;
    private IHealth health;
    private int attack;
    private MovePattern movePattern;
    private IUnityObject _unityObject;
    
    protected BaseUnit(string name, Faction faction, int maxHealth, int attack, MovePattern movePattern, UnitSettings unitSettings)
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
        this._unityObject = unitSettings.unityObject;
        
        this.faction = faction;
        this.unitElement = unitSettings.unitElement;
        this.attack = attack;
        this.health = new Health(maxHealth, unitSettings.healthView);
    }
    
    public string getName()
    {
        return name;
    }

    public Faction getFaction()
    {
        return faction;
    }

    public Element GetElement()
    {
        return unitElement;
    }

    public List<List<Coordinate>> getMoveSequences()
    {
        return movePattern.moveSequences;
    }

    public IHealth getHealth()
    {
        return health;
    }

    public int GetAttack(Element defendingUnitElem)
    {
       switch(defendingUnitElem)
        {
            case Element.Fire:
                if (this.unitElement == Element.Water) return this.attack * 2;
                else return this.attack;
            case Element.Water:
                if (this.unitElement == Element.Nature) return this.attack * 2;
                else return this.attack;
            case Element.Nature:
                if (this.unitElement == Element.Fire) return this.attack * 2;
                else return this.attack;
            default:
                throw new System.Exception($"Unexpected defending unit elem: {defendingUnitElem}");
        }
    }

    public IUnityObject getUnityObject()
    {
        return _unityObject;
    }
    
    public Tile OccupiedTile { get; set; }
    
}
