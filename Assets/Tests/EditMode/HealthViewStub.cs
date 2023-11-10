using System.Threading.Tasks;

public class HealthViewStub : IHealthView
{
    public Task UpdateHealthBar(float maxHealth, float currentHealth)
    {
        return Task.CompletedTask;
    }
}
