using View;
using View.UI;

namespace GameLogic
{
    namespace Factory
    {
        public class HeroSettings
        {
            public IMainHeroView heroView;
            public IHealthView healthView;

            public HeroSettings(IMainHeroView heroView, IHealthView healthView)
            {
                this.heroView = heroView;
                this.healthView = healthView;
            }
        }
    }
}