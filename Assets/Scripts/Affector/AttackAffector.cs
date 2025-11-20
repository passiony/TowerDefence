using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻击Affector
/// </summary>
public class AttackAffector : Affector
{
    public GameObject projectile;

    public Transform[] projectilePoints;

    public bool isMultiAttack;
    
    public float fireRate;
    
    public Targetter towerTargetter;

    protected ILauncher m_Launcher;
    
    protected float m_FireTimer;

    protected Targetable m_TrackingEnemy;

    public override void Initialize(ECamp affectorCamp, LayerMask mask)
    {
        base.Initialize(affectorCamp, mask);
        towerTargetter.camp = affectorCamp;
        m_Launcher = GetComponent<ILauncher>();
        m_FireTimer = 1 / fireRate;

        towerTargetter.findTarget += OnFindTarget;
        towerTargetter.lostTarget += OnLostTarget;
    }

    private void OnDestroy()
    {
        towerTargetter.findTarget -= OnFindTarget;
        towerTargetter.lostTarget -= OnLostTarget;
    }

    private void OnFindTarget(Targetable obj)
    {
        m_TrackingEnemy = obj;
    }
    private void OnLostTarget()
    {
        m_TrackingEnemy = null;
    }

    protected virtual void Update()
    {
        m_FireTimer -= Time.deltaTime;
        if (m_TrackingEnemy != null && m_FireTimer <= 0.0f)
        {
            FireProjectile();
            m_FireTimer = 1 / fireRate;
        }
    }
    
    protected virtual void FireProjectile()
    {
        if (m_TrackingEnemy == null)
        {
            return;
        }

        if (isMultiAttack)
        {
            List<Targetable> enemies = towerTargetter.GetAllTargets();
            m_Launcher.Launch(enemies, projectile, projectilePoints);
        }
        else
        {
            m_Launcher.Launch(m_TrackingEnemy, projectile, projectilePoints);
        }
    }
}