using System.Threading.Tasks;
using UnityEngine;

public interface IUnitView
{
    Task MoveAnimation(int moveToX, int moveToY);
    Task FightAnimation(BaseUnit defendingUnit);
    void DisableFightAnimation();
    void Destroy();
    void SetPosition(Vector2 position);
    void StartFight();
}