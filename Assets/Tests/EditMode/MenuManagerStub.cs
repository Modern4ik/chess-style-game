using System.Threading.Tasks;

public class MenuManagerStub : IMenuManager
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
