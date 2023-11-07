using System.Collections;
using System.Collections.Generic;

public class Health : IHealth
{
    private int currentHealth;
    private int maxHealth;
    private Defense defense;
    private IHealthView _healthView;

    public Health(int maxHealth, Defense defense, IHealthView healthView) {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.defense = defense;
        this._healthView = healthView;
    }

    public Health(int maxHealth, ElementalType elementalType, IHealthView healthView)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this._healthView = healthView;

        switch (elementalType)
        {
            case ElementalType.Fire:
                this.defense = new Defense(0, 1, 0, 0);
                break;
            case ElementalType.Water:
                this.defense = new Defense(0, 0, 1, 0);
                break;
            case ElementalType.Nature:
                this.defense = new Defense(0, 0, 0, 1);
                break;
        }
    }

    public int RecieveDamage(Attack unitAttack)
    {
        int health = 0;

        switch(unitAttack.elementalType)
        {
            case ElementalType.Fire:
                health = currentHealth - (unitAttack.attackPower - this.defense.fireResist);
                break;
            case ElementalType.Water:
                health = currentHealth - (unitAttack.attackPower - this.defense.waterResist);
                break;
            case ElementalType.Nature:
                health = currentHealth - (unitAttack.attackPower - this.defense.natureResist);
                break;
        }

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
