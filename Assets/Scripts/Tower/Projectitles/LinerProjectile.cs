using System;
using UnityEngine;

/// <summary>
/// 简单的IProjectile实现，用于直线飞行的抛射体，可选择性地受到m_Acceleration影响。
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class LinearProjectile : MonoBehaviour, IProjectile
{
    public float startSpeed;

    protected Rigidbody m_Rigidbody;

    public event Action fired;

    protected virtual void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void Fire(Vector3 firingVector)
    {
        transform.rotation = Quaternion.LookRotation(firingVector);
        m_Rigidbody.velocity = firingVector;

        fired?.Invoke();
    }
}