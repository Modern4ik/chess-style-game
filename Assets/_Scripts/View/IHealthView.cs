using System.Threading.Tasks;

namespace View
{
    public interface IHealthView
    {
        Task UpdateHealthBar(float maxHealth, float currentHealth);
    }
}