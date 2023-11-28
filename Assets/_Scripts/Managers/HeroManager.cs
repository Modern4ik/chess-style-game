using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public static HeroManager Instance;

    public IMainHeroView playerHeroView { get; private set; }
    public IMainHeroView opponentHeroView { get; private set; }

    public bool isPlayerDead;
    public bool isOpponentDead;

    [SerializeField] private GameObject playerHeroPrefab;
    [SerializeField] private GameObject opponentHeroPrefab;

    private void Awake()
    {
        Instance = this;

        playerHeroView = Instantiate(playerHeroPrefab).transform.GetComponent<MainHeroView>();
        opponentHeroView = Instantiate(opponentHeroPrefab).transform.GetComponent<MainHeroView>();
    }
}