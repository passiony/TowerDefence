using System;
using UnityEngine;

public class HItscanProjectitle : Projectile
{
    protected Targetable m_Enemy;

    protected void DealDamage()
    {
        if (m_Enemy == null)
        {
            return;
        }
        m_Enemy.TakeDamage(damager.damageAmout, m_Enemy.position, damager.camp);
        Destroy(gameObject);
    }

    public void FireAtTarget(Vector3 startPosition, Targetable enemy)
    {
        m_Enemy = enemy;
        DealDamage();
    }
}