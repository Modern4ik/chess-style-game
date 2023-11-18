public class UnitSettings
{
    public IUnitView unitView;
    public IHealthView healthView;
    public ElementalType unitElement;

    public UnitSettings(IUnitView unitView, IHealthView healthView, ElementalType unitElement)
    {
        this.unitView = unitView;
        this.healthView = healthView;
        this.unitElement = unitElement;
    }
}
