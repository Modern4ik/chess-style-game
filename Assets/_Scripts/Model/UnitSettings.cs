public class UnitSettings
{
    public IUnityObject unityObject;
    public IHealthView healthView;
    public Element unitElement;

    public UnitSettings(IUnityObject unityObject, IHealthView healthView, Element unitElement)
    {
        this.unityObject = unityObject;
        this.healthView = healthView;
        this.unitElement = unitElement;
    }
}
