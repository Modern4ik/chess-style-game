using System.Threading.Tasks;

public class HealthView : IHealthView
{
    private HealthBar healthBar;

    public HealthView(HealthBar healthBar)
    {
        this.healthBar = healthBar;
    }

    public async Task UpdateHealthBar(float maxHealth, float currentHealth)
    {
        await healthBar.UpdateHealthBarSprite(maxHealth, currentHealth);
    }
}
