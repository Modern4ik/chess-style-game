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

    public void SetPosition(Vector2 position)
    {
        _monoBehaviour.transform.position = position;
    }

    public Animator GetAnimator() => _monoBehaviour.transform.GetComponent<Animator>();
    public float GetCoordX() => _monoBehaviour.transform.position.x;
    public float GetCoordY() => _monoBehaviour.transform.position.y;
}
