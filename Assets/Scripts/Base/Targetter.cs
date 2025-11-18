using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetter : MonoBehaviour
{
    public Transform turret;
    public ECamp camp;
    
    protected Collider attachedCollider;
    protected List<Targetable> m_TargetsInRange = new List<Targetable>();
    protected Targetable m_CurrrentTargetable;

    public event Action<Targetable> targetEntersRange;
    public event Action<Targetable> targetExitsRange;
    public event Action<Targetable> acquiredTarget;
    public event Action lostTarget;
    
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

    public Targetable GetNearestTargetable(Vector3 position)
    {
        Targetable nearest = null;
        float minDistSq = float.MaxValue;
        foreach (var targetable in m_TargetsInRange)
        {
            if (targetable == null)
                continue;
            float distSq = (targetable.transform.position - position).sqrMagnitude;
            if (distSq < minDistSq)
            {
                minDistSq = distSq;
                nearest = targetable;
            }
        }

        return nearest;
    }
}