using System.Collections.Generic;
using System.Linq;

public abstract class BaseUnit
{
    private string name;
    private Faction faction;
    private ElementalType unitElement;
    private Attack attack;
    private IHealth health;
    private MovePattern movePattern;
    private IUnitView _unitView;
    
    protected BaseUnit(string name, Faction faction, int maxHealth, int attack, MovePattern movePattern, UnitSettings unitSettings)
    {
        this.name = name;
        this.faction = faction;
        this.unitElement = unitSettings.unitElement;
        this._unitView = unitSettings.unitView;
        
        this.attack = new Attack(unitElement, attack);
        this.health = new Health(maxHealth, CreateDefenseStats(unitElement), unitSettings.healthView);
        this.movePattern = CreateMovePattern(faction, movePattern);
    }
    
    public string getName()
    {
        return name;
    }

    public Faction getFaction()
    {
        return faction;
    }

    public ElementalType GetElement()
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

    public Attack GetAttack()
    {
        return attack;
    }

    public IUnitView getUnitView()
    {
        return _unitView;
    }
    
    public Tile OccupiedTile { get; set; }

    private MovePattern CreateMovePattern(Faction unitFaction, MovePattern movePattern)
    {
        switch (unitFaction)
        {
            case Faction.Enemy:
                return new MovePattern(movePattern.moveSequences.Select(steps => steps.Select(coord => new Coordinate(coord.x, -coord.y)).ToList()).ToList());
            case Faction.Hero:
                return movePattern;
            default:
                throw new System.Exception($"Unexpected faction: {unitFaction}");
        }
    }

    private Defense CreateDefenseStats(ElementalType unitElemType)
    {
        switch(unitElemType)
        {
            case ElementalType.Fire: return new Defense(1, 0, 0);
            case ElementalType.Water: return new Defense(0, 1, 0);
            case ElementalType.Nature: return new Defense(0, 0, 1);
            default: throw new System.Exception($"Unexpected unit elemental type: {unitElemType}");
        }
    }
    
}
