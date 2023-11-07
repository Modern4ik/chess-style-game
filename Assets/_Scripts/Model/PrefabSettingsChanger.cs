using UnityEngine;

public static class PrefabSettingsChanger
{
    public static Color SetRandomColor()
    {
        switch (Random.Range(0, 3))
        {
            case 0: return Color.red;
            case 1: return Color.blue;
            case 2: return Color.green;
            default: throw new System.Exception("Color out of range");
        }
    }
}