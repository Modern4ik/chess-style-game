public class Attack
{
    public ElementalType elementalType { get; }
    public int attackPower { get; }

    public Attack(ElementalType elementalType, int attackPower)
    {
        this.elementalType = elementalType;
        this.attackPower = attackPower;
    }
}