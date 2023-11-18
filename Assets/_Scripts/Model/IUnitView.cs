using System.Threading.Tasks;
using UnityEngine;

public interface IUnitView
{
    Task StartMoveAnimation(int moveToX, int moveToY);
    Task StartFightAnimation(BaseUnit defendingUnit);
    void DisableFightAnimation();
    void Destroy();
    void SetPosition(Vector2 position);
    void StartFight();
}