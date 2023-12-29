using System.Threading.Tasks;
using GameLogic;

namespace View
{
    public interface IMainHeroView
    {
        public bool isHeroDead { get; }
        public Task GetDamage(Attack unitAttack);
        public void SetUnderAttackMark(bool isEnable);
    }
}