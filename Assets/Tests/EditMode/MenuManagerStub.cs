using GameLogic;
using GameLogic.Units;
using System.Threading.Tasks;

namespace SuperUserInput
{
    public class MenuManagerStub : ISystemMenuManager
    {
        public Task DoDamageToMainHero(Faction unitFaction, Attack unitAttack)
        {
            throw new System.NotImplementedException();
        }

        public void DamageEnemy(int damage)
        {
            throw new System.NotImplementedException();
        }

        public void ShowTileInfo(GameTile tile)
        {
            throw new System.NotImplementedException();
        }

    }
}