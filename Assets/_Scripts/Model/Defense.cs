public class Defense
{
    public int armor { get; }
    public int fireResist { get; }
    public int waterResist { get; }
    public int natureResist { get; }

    public Defense(int armor, int fireResist, int waterResist, int natureResist)
    {
        this.armor = armor;
        this.fireResist = fireResist;
        this.waterResist = waterResist;
        this.natureResist = natureResist;
    }
}