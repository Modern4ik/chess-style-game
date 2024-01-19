using GameSettings;
using UnityEngine;

namespace View
{
    namespace UI
    {
        public class SkipButtonView : MonoBehaviour
        {
            public void SkipChoiceTurn()
            {
                if (GameStatus.isAwaitPlayerInput) GameStatus.isChoiceSkipped = true;
            }
        }
    }
}