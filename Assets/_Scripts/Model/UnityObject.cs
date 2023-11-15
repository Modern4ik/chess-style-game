using UnityEngine;

public class UnityObject : IUnityObject
{
    private MonoBehaviour _monoBehaviour;
    
    public UnityObject(MonoBehaviour monoBehaviour)
    {
        this._monoBehaviour = monoBehaviour;
    }
    
    public void Destroy()
    {
        Object.Destroy(_monoBehaviour.gameObject);
    }

    public void SetPosition(Vector3 position)
    {
        _monoBehaviour.transform.position = position;
    }

    public Transform GetTransform() => _monoBehaviour.transform;
}
