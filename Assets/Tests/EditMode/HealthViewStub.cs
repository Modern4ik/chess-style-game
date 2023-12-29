using System.Threading.Tasks;

namespace View
{
    public class HealthViewStub : IHealthView
    {
        public Task UpdateHealthBar(float maxHealth, float currentHealth)
        {
            return Task.CompletedTask;
        }
    }
}