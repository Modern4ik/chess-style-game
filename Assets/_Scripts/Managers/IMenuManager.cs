using System.Threading.Tasks;

public interface IMenuManager
{
    public Task DoDamageToMainHero(Faction unitFaction, Attack unitAttack);
    public void ShowTileInfo(Tile tile);
}