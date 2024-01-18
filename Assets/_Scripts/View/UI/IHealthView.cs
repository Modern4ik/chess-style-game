using System.Threading.Tasks;

namespace View
{
    namespace UI
    {
        public interface IHealthView
        {
            Task UpdateHealthBar(float maxHealth, float currentHealth);
        }
    }
}