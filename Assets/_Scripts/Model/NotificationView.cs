using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationView : MonoBehaviour, INotificationView
{
    public static NotificationView Instance;

    private float _targetTransparency = 0f;
    private bool isShowing = false;
    private Image _notificationBorder;
    private TextMeshProUGUI _notificationText;

    [SerializeField] private float _reduceTransparencySpeed;
    [SerializeField] private int _notificationDelay;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {   
        if (isShowing)
        {
            _notificationBorder.color = new Color(
                1f, 1f, 1f, Mathf.MoveTowards(_notificationBorder.color.a, _targetTransparency, _reduceTransparencySpeed * Time.deltaTime));

            _notificationText.color = new Color(
                _notificationText.color.r, _notificationText.color.g, _notificationText.color.b, Mathf.MoveTowards(_notificationText.color.a, _targetTransparency, _reduceTransparencySpeed * Time.deltaTime));
        }    
    }

    public async Task StartNotificationAnimation()
    {
        _notificationBorder = this.transform.Find("Border").GetComponent<Image>();
        _notificationText = GetNotificationText();
        await Task.Delay(_notificationDelay);

        bool isEqualsValues = false;
        isShowing = true;

        while (!isEqualsValues)
        {
            isEqualsValues = _notificationBorder.color.a == _targetTransparency;
            await Task.Delay(25);
        }

        isShowing = false;
    }

    private TextMeshProUGUI GetNotificationText()
    {
        switch (GameManager.Instance.GameState)
        {
            case GameState.SpawnHeroes:
                GameObject playerText = this.transform.Find("PlayerTurnText").gameObject;
                playerText.SetActive(true);

                return playerText.GetComponent<TextMeshProUGUI>();
            case GameState.SpawnEnemies:
                GameObject enemyText = this.transform.Find("EnemyTurnText").gameObject;
                enemyText.SetActive(true);

                return enemyText.GetComponent<TextMeshProUGUI>();
            default:
                throw new System.Exception($"GameState out of range: {GameManager.Instance.GameState}");
        }
    }
}