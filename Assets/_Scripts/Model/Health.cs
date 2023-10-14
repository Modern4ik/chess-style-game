using System.Collections;
using System.Collections.Generic;

public class Health
{
    private int currentHealth;
    private int maxHealth;

    public Health(int maxHealth) {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
    }

    public int GetDamage(int damage)
    {
        int health = currentHealth - damage;
        if (health < 0) health = 0;
        currentHealth = health;
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
