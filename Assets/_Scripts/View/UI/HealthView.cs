using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    namespace UI
    {
        public class HealthView : MonoBehaviour, IHealthView
        {
            private float _targetHealth = 1;
            private float _reduceSpeed = 0.7f;

            [SerializeField] private Image _healthBarSprite;

            public async Task UpdateHealthBar(float maxHealth, float currentHealth)
            {
                _targetHealth = currentHealth / maxHealth;

                bool isEqualsValues = false;

                while (!isEqualsValues)
                {
                    isEqualsValues = _healthBarSprite.fillAmount == _targetHealth;
                    await Task.Delay(25);
                }
            }

            private void Update()
            {
                _healthBarSprite.fillAmount = Mathf.MoveTowards(_healthBarSprite.fillAmount, _targetHealth, _reduceSpeed * Time.deltaTime);
            }
        }
    }
}
