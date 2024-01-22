using View;

namespace GameLogic
{
    namespace Fruits
    {
        public class BaseFruit
        {
            public IFruitView fruitView { get; }

            public BaseFruit(IFruitView fruitView)
            {
                this.fruitView = fruitView;
            }
        }
    }
}