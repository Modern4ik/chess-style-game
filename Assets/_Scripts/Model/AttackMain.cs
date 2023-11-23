public class AttackMain : UnitMove
{
    public IMainHeroView mainHeroToAttack { get; set; }

    public AttackMain(IMainHeroView mainHeroToAttack) => this.mainHeroToAttack = mainHeroToAttack;
}