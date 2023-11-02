using System.Collections;
using System.Collections.Generic;

public class Health : IHealth
{
    private int currentHealth;
    private int maxHealth;
    private IHealthView _healthView;

    public Health(int maxHealth, IHealthView healthView) {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this._healthView = healthView;
    }

    public int RecieveDamage(int damage)
    {
        int health = currentHealth - damage;
        if (health < 0) health = 0;
        currentHealth = health;
        _healthView.UpdateUnitHealthBar(maxHealth, currentHealth);
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
