using System.Threading.Tasks;
using UnityEngine;
using GameLogic;

namespace View
{
    public class MainHeroView : MonoBehaviour, IMainHeroView
    {
        public bool isHeroDead { get; private set; }

        private IHealth heroHealth;

        [SerializeField] private int healthValue;

        private void Awake()
        {
            heroHealth = new Health(healthValue, new Defense(1, 1, 1), this.transform.Find("HeroCanvas/HealthBar").GetComponent<HealthView>());
        }

        public async Task GetDamage(Attack unitAttack)
        {
            if (await heroHealth.RecieveDamage(unitAttack) == 0)
            {
                switch (this.tag)
                {
                    case "Player":
                        HeroManager.Instance.isPlayerDead = true;
                        break;
                    case "Opponent":
                        HeroManager.Instance.isOpponentDead = true;
                        break;
                }
            }
        }

        public void SetUnderAttackMark(bool isEnable) => this.transform.Find("HeroCanvas/AttackMarker").gameObject.SetActive(isEnable);
    }
}