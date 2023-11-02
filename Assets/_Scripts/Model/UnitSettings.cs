using UnityEngine.UI;

public class UnitSettings
{
    public IUnityObject unityObject;
    public IHealthView healthView;

    public UnitSettings(IUnityObject unityObject, IHealthView healthView)
    {
        this.unityObject = unityObject;
        this.healthView = healthView;
    }
}
