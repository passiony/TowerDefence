using System;
using UnityEngine;

/// <summary>
/// 可受伤物体
/// </summary>
public class DamageableBehaviour : MonoBehaviour
{
    public Damageable configuration;

    public event Action<HitInfo> hit;

    public event Action<DamageableBehaviour> removed;

    public event Action<DamageableBehaviour> died;

    protected virtual void Awake()
    {
        configuration.Init();
        configuration.died += OnConfigurationDied;
    }

    public virtual void TakeDamage(float damageValue, Vector3 damagePoint, ECamp camp)
    {
        HealthChangeInfo info;
        configuration.TakeDamage(damageValue, camp, out info);
        var damageInfo = new HitInfo(info, damagePoint);
        hit?.Invoke(damageInfo);
    }

    void OnConfigurationDied(HealthChangeInfo changeInfo)
    {
        OnDeath();
        OnRemoved();
    }

    public virtual void OnDeath()
    {
        died?.Invoke(this);
    }
    
    public virtual void OnRemoved()
    {
        configuration.SetHealth(0);
        removed?.Invoke(this);
    }
}