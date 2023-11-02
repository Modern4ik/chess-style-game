using UnityEngine;
using UnityEngine.UI;

public static class UnitSettingsCreator
{
    public static UnitSettings createUnitSettings(MonoBehaviour monoBehaviour)
    {
        IUnityObject unityObject = new UnityObject(monoBehaviour);
        Image healthBar = monoBehaviour.transform.Find("UnitCanvas/HealthBar/Foreground").
            GetComponent<Image>();
        IHealthView healthView = new HealthView(healthBar);
        return new UnitSettings(unityObject, healthView);
    }        
}