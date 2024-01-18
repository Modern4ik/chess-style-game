using UnityEngine;
using View;
using View.UI;

namespace GameLogic
{
    namespace Factory
    {
        public static class HeroSettingsCreator
        {
            public static HeroSettings CreateHeroSettings(GameObject heroPrefab)
            {
                IMainHeroView heroView = heroPrefab.transform.GetComponent<MainHeroView>();
                HealthView healthView = heroPrefab.transform.Find("HeroCanvas/HealthBar").GetComponent<HealthView>();

                // TODO: Возможно в будущем у нас появится PsyEnergy у OpponentHero, тогда добавим в конструктор
                // HeroSettings также поле PsyEnergy.

                return new HeroSettings(heroView, healthView);
            }
        }
    }
}