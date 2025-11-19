using System;
using UnityEngine;

public class HItscanProjectitle : MonoBehaviour
{
    protected Targetable m_Enemy;
    protected Damager m_Damager;

    protected virtual void Awake()
    {
        m_Damager = GetComponent<Damager>();
    }


    protected void DealDamage()
    {
        if (m_Enemy == null)
        {
            return;
        }

        m_Enemy.TakeDamage(m_Damager.damage, m_Enemy.position, m_Damager.camp);
        Destroy(gameObject);
    }

    public void FireAtTarget(Vector3 startPosition, Targetable enemy)
    {
        m_Enemy = enemy;
        DealDamage();
    }
}