using UnityEngine;

namespace View
{
    public class MainHeroView : MonoBehaviour, IMainHeroView
    {
        public int healthCount { get; private set; }

        [SerializeField] private int healthValue;

        private void Awake()
        {
            healthCount = healthValue;
        }

        public void SetUnderAttackMark(bool isEnable) => this.transform.Find("HeroCanvas/AttackMarker").gameObject.SetActive(isEnable);
    }
}