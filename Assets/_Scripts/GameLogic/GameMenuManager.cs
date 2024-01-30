using GameLogic.Units;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using View.UI;

public class GameMenuManager : MonoBehaviour
{
    public static GameMenuManager Instance;

    private UnitMenuView unitMenuView;
    private SkipButtonView skipButtonView;
    
    [SerializeField] private UnitMenuView _unitSelectMenuPrefab;
    [SerializeField] private NotificationView _turnNotificationPrefab;
    [SerializeField] private SkipButtonView _skipButtonPrefab;
    [SerializeField] private GameObject _generalCanvas;

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateUnitSelectMenu()
    {
        unitMenuView = Instantiate(_unitSelectMenuPrefab, _generalCanvas.transform);
        UpdateUnitSelectMenu();
        GenerateSkipButton();
    }

    public void UpdateUnitSelectMenu() => unitMenuView.UpdateUnitMenuView();

    public async Task GenerateTurnNotification(Faction faction)
    {
        await Task.Delay(1500);

        NotificationView turnNotification = Instantiate(_turnNotificationPrefab, _generalCanvas.transform);
        turnNotification.transform.SetAsFirstSibling();
        await turnNotification.StartNotificationAnimation(faction);

        Destroy(turnNotification.gameObject);
    }

    public void ActivateSkipButton() => skipButtonView.GetComponent<Image>().enabled = true;

    public void DeactivateSkipButton() => skipButtonView.GetComponent<Image>().enabled = false;

    private void GenerateSkipButton() => skipButtonView = Instantiate(_skipButtonPrefab, _generalCanvas.transform);
}