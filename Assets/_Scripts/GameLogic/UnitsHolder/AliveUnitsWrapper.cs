using GameLogic.Units;

namespace GameLogic
{
    namespace Holders
    {
        public class AliveUnitWrapper
        {
            private bool _isAlive = true;
            private BaseUnit baseUnityUnit;
            public AliveUnitWrapper(BaseUnit baseUnityUnit)
            {
                this.baseUnityUnit = baseUnityUnit;
                _isAlive = true;
            }

            public bool isAlive()
            {
                return _isAlive;
            }

            public void MarkForDeletion()
            {
                _isAlive = false;
            }

            public BaseUnit getUnit()
            {
                return baseUnityUnit;
            }
        }
    }
}
