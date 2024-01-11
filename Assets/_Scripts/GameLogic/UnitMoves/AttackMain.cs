using GameLogic.Heroes;
using View;

namespace GameLogic
{
    namespace UnitMoves
    {
        public class AttackMain : UnitMove
        {
            public BaseHero mainHeroToAttack { get; set; }

            public AttackMain(BaseHero mainHeroToAttack) => this.mainHeroToAttack = mainHeroToAttack;
        }
    } 
}