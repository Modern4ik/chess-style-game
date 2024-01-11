using GameLogic.Factory;
using GameLogic.Units;

namespace GameLogic
{
    namespace Heroes
    {
        public class PlayerHero : BaseHero
        {
            public PlayerHero(string name, Faction faction, HeroSettings heroSettings) : base(name, faction, heroSettings) { }
        }
    }
}