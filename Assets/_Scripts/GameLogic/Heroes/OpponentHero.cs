using GameLogic.Factory;
using GameLogic.Units;

namespace GameLogic
{
    namespace Heroes
    {
        public class OpponentHero : BaseHero
        {
            public OpponentHero(string name, Faction faction, HeroSettings heroSettings) : base (name, faction, heroSettings) { }
        }
    }
}