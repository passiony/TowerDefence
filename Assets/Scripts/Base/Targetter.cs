using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetter : MonoBehaviour
{
    public Transform turret;
    public ECamp camp;

    protected Collider attachedCollider;
    public List<Targetable> m_TargetsInRange = new List<Targetable>();
    protected Targetable m_CurrrentTargetable;

    public event Action<Targetable> targetEntersRange;
    public event Action<Targetable> targetExitsRange;
    public event Action<Targetable> findTarget;
    public event Action lostTarget;

    public float searchRate;
    protected float m_SearchTimer = 0.0f;
    protected bool m_HadTarget;

    public float effectRadius
    {
        get
        {
            var sphere = attachedCollider as SphereCollider;
            if (sphere != null)
            {
                return sphere.radius;
            }

            var capsule = attachedCollider as CapsuleCollider;
            if (capsule != null)
            {
                return capsule.radius;
            }

            return 0;
        }
    }

    void Awake()
    {
        attachedCollider = GetComponent<Collider>();
        attachedCollider.isTrigger = true;
    }

    public Targetable GetTarget()
    {
        return m_CurrrentTargetable;
    }

    public List<Targetable> GetAllTargets()
    {
        return m_TargetsInRange;
    }


    void OnTriggerEnter(Collider other)
    {
        var targetable = other.GetComponent<Targetable>();
        if (!IsTargetableValid(targetable))
        {
            return;
        }

        m_TargetsInRange.Add(targetable);
        targetEntersRange?.Invoke(targetable);
    }

    void OnTriggerExit(Collider other)
    {
        var targetable = other.GetComponent<Targetable>();
        if (!IsTargetableValid(targetable))
        {
            return;
        }

        m_TargetsInRange.Remove(targetable);
        targetExitsRange?.Invoke(targetable);

        if (targetable == m_CurrrentTargetable)
        {
            OnTargetRemove();
        }
    }

    protected virtual bool IsTargetableValid(Targetable targetable)
    {
        if (targetable == null)
        {
            return false;
        }

        bool canDamage = camp != targetable.configuration.camp;
        return canDamage;
    }

    public Targetable GetNearestTargetable()
    {
        Targetable nearest = null;
        float minDistSq = float.MaxValue;
        foreach (var targetable in m_TargetsInRange)
        {
            if (targetable == null)
                continue;
            float distSq = (targetable.transform.position - transform.position).sqrMagnitude;
            if (distSq < minDistSq)
            {
                minDistSq = distSq;
                nearest = targetable;
            }
        }

        return nearest;
    }


    private void Update()
    {
        m_SearchTimer -= Time.deltaTime;

        if (m_SearchTimer <= 0.0f && m_CurrrentTargetable == null && m_TargetsInRange.Count > 0)
        {
            m_CurrrentTargetable = GetNearestTargetable();
            if (m_CurrrentTargetable != null)
            {
                if (findTarget != null)
                {
                    findTarget(m_CurrrentTargetable);
                }

                m_SearchTimer = searchRate;
            }
        }

        AimTurret();

        m_HadTarget = m_CurrrentTargetable != null;
    }

    public void AimTurret()
    {
        if (m_CurrrentTargetable == null)
        {
            return;
        }

        Vector3 targetPosition = m_CurrrentTargetable.position;
        Vector3 direction = targetPosition - turret.position;
        Quaternion look = Quaternion.LookRotation(direction, Vector3.up);
        Vector3 lookEuler = look.eulerAngles;
        look.eulerAngles = lookEuler;
        turret.rotation = look;
    }
    
    void OnTargetRemove()
    {
        lostTarget?.Invoke();
        m_TargetsInRange.Remove(m_CurrrentTargetable);
        m_CurrrentTargetable = null;
    }
}