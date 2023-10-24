using UnityEngine;

[CreateAssetMenu(fileName = "New Unit",menuName = "Scriptable Unit")]
public class ScriptableUnit : ScriptableObject {
    //Предполагаю что можно использовать просто MonoBehaviour
    // т.к до этого тут использовался BaseUnityUnit, который наследовался от MonoBehaviour
    public EmptyUnityObject UnitPrefab;
}