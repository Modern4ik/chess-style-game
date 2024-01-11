using GameLogic.Factory;
using GameLogic.Units;
using View;


namespace GameLogic
{
    namespace Heroes
    {
        public class BaseHero
        {
            public string name { get; }
            public Faction faction { get; }
            public IHealth health { get; }
            public IMainHeroView heroView { get; }
            public IPsyEnergy psyEnergy { get; set; }

            protected BaseHero(string name, Faction faction, HeroSettings heroSettings)
            {
                this.name = name;
                this.faction = faction;
                heroView = heroSettings.heroView;
                health = new Health(heroView.healthCount, new Defense(1, 1, 1), heroSettings.healthView);
            }
        }
    }
}