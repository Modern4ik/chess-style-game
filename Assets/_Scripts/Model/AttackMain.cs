public class AttackMain : UnitMove
{
    public bool isAttackOpponentMainHealth { get; set; }
    public bool isAttackHeroMainHealth { get; set; }

    public AttackMain(int coordY)
    {
        if (coordY > GridSettings.HEIGHT - 1) isAttackOpponentMainHealth = true;
        else isAttackHeroMainHealth = true;
    }
}