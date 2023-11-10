using System.Threading.Tasks;

public interface IHealth
{
    public int GetMaxHealth();
    public Task<int> RecieveDamage(Attack unitAttack);
    public int GetCurrentHealth();
}
