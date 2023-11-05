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
        Element unitElement = GetUnitElement(monoBehaviour.GetComponent<SpriteRenderer>().color);
        return new UnitSettings(unityObject, healthView, unitElement);
    }
    
    private static Element GetUnitElement(Color unitColor)
    {   
        if (unitColor == Color.red) return Element.Fire;
        else if (unitColor == Color.blue) return Element.Water;
        else if (unitColor == Color.green) return Element.Nature;
        else throw new System.Exception($"Unexpected unit color: {unitColor}");
    }
}