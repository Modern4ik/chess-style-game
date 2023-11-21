using System.Threading.Tasks;

public interface IMainHeroView
{
    public Task GetDamage(Attack unitAttack);
    public void EnableAttackMark(bool isEnable);
}