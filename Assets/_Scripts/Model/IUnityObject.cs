using UnityEngine;

public interface IUnityObject
{
    public void Destroy();
    public void SetPosition(Vector2 position);
    public Animator GetAnimator();
    public float GetCoordX();
    public float GetCoordY();
}
