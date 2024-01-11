using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{   
    namespace UI
    {
        public class PsyEnergyView : MonoBehaviour, IPsyEnergyView
        {
            private float targetEnergy = 1;
            private float reduceSpeed = 0.7f;

            [SerializeField] private Image _psyEnergySprite;
            [SerializeField] private TextMeshProUGUI _psyEnergyText;

            public void UpdatePsyEnergyBar(float maxEnergy, float currentEnergy)
            {
                targetEnergy = currentEnergy / maxEnergy;
                _psyEnergyText.text = $"{currentEnergy}/{maxEnergy}";
            }

            private void Update()
            {
                _psyEnergySprite.fillAmount = Mathf.MoveTowards(_psyEnergySprite.fillAmount, targetEnergy, reduceSpeed * Time.deltaTime);
            }
        }
    } 
}