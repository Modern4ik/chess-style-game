using System.Threading.Tasks;
using UnityEngine;

public class MainHeroView : MonoBehaviour, IMainHeroView
{
    public bool isHeroDead { get; private set; }

    private IHealth heroHealth;

    private void Awake()
    {
        heroHealth = new Health(1, new Defense(1, 1, 1), this.transform.Find("HeroCanvas/HealthBar").GetComponent<HealthView>());
    }

    public async Task GetDamage(Attack unitAttack)
    {
        if (await heroHealth.RecieveDamage(unitAttack) == 0) isHeroDead = true;
    }

    public void SetUnderAttackMark(bool isEnable) => this.transform.Find("HeroCanvas/AttackMarker").gameObject.SetActive(isEnable);
}