public interface IHealth
{
    public int GetMaxHealth();
    public int RecieveDamage(Attack unitAttack);
    public int GetCurrentHealth();
}
