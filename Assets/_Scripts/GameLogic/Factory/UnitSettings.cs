using View;
using View.UI;
using GameLogic.Units;

namespace GameLogic
{   
    namespace Factory
    {
        public class UnitSettings
        {
            public IUnitView unitView;
            public IHealthView healthView;
            public ElementalType unitElement;

            public UnitSettings(IUnitView unitView, IHealthView healthView, ElementalType unitElement)
            {
                this.unitView = unitView;
                this.healthView = healthView;
                this.unitElement = unitElement;
            }
        }
    }
}