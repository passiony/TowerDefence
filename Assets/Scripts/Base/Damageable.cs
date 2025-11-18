using System;
using UnityEngine;

[Serializable]
public class Damageable
{
    public float maxHealth;
    public float startingHealth;
    public ECamp camp;

    public float currentHealth { protected set; get; }

    public float normalisedHealth => currentHealth / maxHealth;
    public bool isDead => currentHealth <= 0f;
    
    public bool isAtMaxHealth => Mathf.Approximately(currentHealth, maxHealth);

    public event Action reachedMaxHealth;

    public event Action<HealthChangeInfo> damaged, healed, died, healthChanged;

    public virtual void Init()
    {
        currentHealth = startingHealth;
    }

    public void SetMaxHealth(float health)
    {
        if (health <= 0)
        {
            return;
        }

        maxHealth = startingHealth = health;
    }

    public void SetMaxHealth(float health, float startingHealth)
    {
        if (health <= 0)
        {
            return;
        }

        maxHealth = health;
        this.startingHealth = startingHealth;
    }

    public void SetHealth(float health)
    {
        var info = new HealthChangeInfo
        {
            damageable = this,
            newHealth = health,
            oldHealth = currentHealth
        };

        currentHealth = health;

        if (healthChanged != null)
        {
            healthChanged(info);
        }
    }

    public bool TakeDamage(float damage, ECamp damageCamp, out HealthChangeInfo output)
    {
        output = new HealthChangeInfo
        {
            damageCamp = damageCamp,
            damageable = this,
            newHealth = currentHealth,
            oldHealth = currentHealth
        };

        bool canDamage = damageCamp != camp;

        if (isDead || !canDamage)
        {
            return false;
        }

        ChangeHealth(-damage, output);
        damaged?.Invoke(output);
        if (isDead)
        {
            died?.Invoke(output);
        }

        return true;
    }

    public HealthChangeInfo IncreaseHealth(float health)
    {
        var info = new HealthChangeInfo { damageable = this };
        ChangeHealth(health, info);
        healed?.Invoke(info);
        if (isAtMaxHealth)
        {
            reachedMaxHealth?.Invoke();
        }
        return info;
    }

    protected void ChangeHealth(float healthIncrement, HealthChangeInfo info)
    {
        info.oldHealth = currentHealth;
        currentHealth += healthIncrement;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        info.newHealth = currentHealth;

        if (healthChanged != null)
        {
            healthChanged(info);
        }
    }
}