using System;
using System.Collections.Generic;
using UnityEngine;

public class BallisticProjectile : MonoBehaviour, IProjectile
{
    protected Rigidbody m_Rigidbody;
    protected Collider[] m_Colliders;
    
    [Range(-90, 90)]
    public float firingAngle;

    public float startSpeed;
    private bool m_Fired;

    public event Action fired;
    
    protected virtual void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Colliders = GetComponentsInChildren<Collider>();
    }
    
    public void FireAtPoint(Vector3 startPoint, Vector3 targetPoint)
    {
        transform.position = startPoint;

        //计算从start到达target位置的子弹发射velocity
        Vector3 firingVector = Ballistics.CalculateBallisticFireVector(startPoint, targetPoint, firingAngle, startSpeed);
        Fire(firingVector);
    }

    protected virtual void Fire(Vector3 firingVector)
    {
        transform.rotation = Quaternion.LookRotation(firingVector);
        m_Rigidbody.velocity = firingVector;
        m_Fired = true;

        fired?.Invoke();
    }
}