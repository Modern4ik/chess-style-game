using UnityEngine.UI;

public class HealthView : IHealthView
{
    private Image healthBar;

    public HealthView(Image healthBar)
    {
        this.healthBar = healthBar;
    }

    public void UpdateUnitHealthBar(int maxHealth, int currentHealth)
    {
        healthBar.fillAmount = (float) currentHealth / maxHealth;
    }
}
