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
    
    public virtual void TakeDamage(float damageValue, Vector3 damagePoint, ECamp camp)
    {
        HealthChangeInfo info;
        configuration.TakeDamage(damageValue, camp, out info);
        var damageInfo = new HitInfo(info, damagePoint);
        if (hit != null)
        {
            hit(damageInfo);
        }
    }

}