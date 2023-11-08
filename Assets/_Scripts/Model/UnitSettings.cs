public class UnitSettings
{
    public IUnityObject unityObject;
    public IHealthView healthView;
    public ElementalType unitElement;

    public UnitSettings(IUnityObject unityObject, IHealthView healthView, ElementalType unitElement)
    {
        this.unityObject = unityObject;
        this.healthView = healthView;
        this.unitElement = unitElement;
    }
}
