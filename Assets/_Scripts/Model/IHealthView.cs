using System.Threading.Tasks;

public interface IHealthView
{
    Task UpdateHealthBar(float maxHealth, float currentHealth);
}
