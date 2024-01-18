using GameLogic.Factory;
using GameLogic.Heroes;
using GameLogic.Units;
using UnityEngine;
using View.UI;

namespace GameLogic
{
    public class HeroManager : MonoBehaviour
    {
        public static HeroManager Instance;

        public BaseHero playerHero { get; private set; }
        public BaseHero opponentHero { get; private set; }

        [SerializeField] private GameObject _playerHeroPrefab;
        [SerializeField] private GameObject _opponentHeroPrefab;

        private void Awake()
        {
            Instance = this;

            GameObject playerHeroObject = Instantiate(_playerHeroPrefab);
            GameObject opponentHeroObject = Instantiate(_opponentHeroPrefab);

            playerHero = new PlayerHero("PlayerHero", Faction.Hero, HeroSettingsCreator.CreateHeroSettings(playerHeroObject));
            playerHero.psyEnergy = new PsyEnergy(5, playerHeroObject.transform.Find("HeroCanvas/PsyEnergy").GetComponent<PsyEnergyView>());

            opponentHero = new OpponentHero("OpponentHero", Faction.Enemy, HeroSettingsCreator.CreateHeroSettings(opponentHeroObject));
        }
    }
}

