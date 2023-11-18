using System.Threading.Tasks;
using UnityEngine;

public class UnitViewStub : IUnitView
{
    public void Destroy() { }
    public void SetPosition(Vector2 position) { }
    public async Task StartFightAnimation(BaseUnit defendingUnit) => await Task.CompletedTask;
    public async Task StartMoveAnimation(int moveToX, int moveToY) => await Task.CompletedTask;
    public void StartFight() { }
    public void DisableFightAnimation() { }
}
