using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    public async Task<int> RecieveDamage(Attack unitAttack)
    {
        int health = currentHealth - (unitAttack.attackPower - GetResist(unitAttack.elementalType));

        if (health < 0) health = 0;
        currentHealth = health;
        await _healthView.UpdateHealthBar(maxHealth, currentHealth);
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

    private int GetResist(ElementalType unitElemType)
    {
        switch (unitElemType)
        {
            case ElementalType.Fire: return this.defense.fireResist;
            case ElementalType.Water: return this.defense.waterResist;
            case ElementalType.Nature: return this.defense.natureResist;
            default: throw new System.Exception($"Unexpected unit element type: {unitElemType}");
        }
    }
}
