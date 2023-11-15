using UnityEngine;

public interface IUnityObject
{
    public void Destroy();
    public void SetPosition(Vector3 position);
    public Transform GetTransform();
}
