using System.Threading.Tasks;

public interface IMainHeroView
{
    public bool isHeroDead { get; }
    public Task GetDamage(Attack unitAttack);
    public void SetUnderAttackMark(bool isEnable);
}