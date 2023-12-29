using System.Threading.Tasks;

namespace GameLogic
{
    public interface IHealth
    {
        public int GetMaxHealth();
        public Task<int> RecieveDamage(Attack unitAttack);
        public int GetCurrentHealth();
    }
}