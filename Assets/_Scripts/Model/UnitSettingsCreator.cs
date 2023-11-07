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
        ElementalType unitElement = GetUnitElement(monoBehaviour.GetComponent<SpriteRenderer>().color);
        return new UnitSettings(unityObject, healthView, unitElement);
    }
    
    private static ElementalType GetUnitElement(Color unitColor)
    {   
        if (unitColor == Color.red) return ElementalType.Fire;
        else if (unitColor == Color.blue) return ElementalType.Water;
        else if (unitColor == Color.green) return ElementalType.Nature;
        else throw new System.Exception($"Unexpected unit color: {unitColor}");
    }
}