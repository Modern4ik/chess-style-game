using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float _targetHealth = 1;
    private float _reduceSpeed = 0.7f;

    [SerializeField] private Image _healthBarSprite;

    public async Task UpdateHealthBarSprite(float maxHealth, float currentHealth)
    {
        _targetHealth = currentHealth / maxHealth;

        bool isEqualsValues = false;

        while (!isEqualsValues) isEqualsValues = await Task.Run(() => _healthBarSprite.fillAmount == _targetHealth);
    }

    private void Update()
    {   
        _healthBarSprite.fillAmount = Mathf.MoveTowards(_healthBarSprite.fillAmount, _targetHealth, _reduceSpeed * Time.deltaTime);
    }
}