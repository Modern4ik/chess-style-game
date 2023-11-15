using System.Threading.Tasks;

public interface IUnitView
{
    Task StartMoveAnimation(BaseUnit unit, int moveToX, int moveToY);
    Task StartFightAnimation(BaseUnit attackingunit, BaseUnit defendingUnit);
    void DisableFightAnimation();
}