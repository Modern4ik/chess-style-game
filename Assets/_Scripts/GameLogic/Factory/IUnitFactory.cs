using UserInput;
using GameLogic.Units;

namespace GameLogic
{   
    namespace Factory
    {
        public interface IUnitFactory
        {
            public BaseUnit createUnit(Faction faction, InputData inputData);
        }
    }
}

