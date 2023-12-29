using GameLogic.Units;
using System.Threading.Tasks;
using UnityEngine;

namespace View
{
    public class UnitViewStub : IUnitView
{
    public void Destroy() { }
    public void SetPosition(Vector2 position) { }
    public async Task FightAnimation(BaseUnit defendingUnit) => await Task.CompletedTask;
    public async Task MoveAnimation(int moveToX, int moveToY) => await Task.CompletedTask;
    public void StartFight() { }
    public void DisableFightAnimation() { }
}
}