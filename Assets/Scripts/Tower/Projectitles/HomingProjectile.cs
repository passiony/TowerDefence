using System;
using UnityEngine;

/// <summary>
/// 简单的Projectile实现，用于直线飞行的抛射体，可选择性地受到m_Acceleration影响。
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class HomingProjectile : Projectile
{
    public float startSpeed;
    public bool explode;
    public float explodeRange = 1.0f;
    public LayerMask explodeMask;
    public ParticleSystem explodeParticle;

    public event Action fired;

    private readonly Collider[] s_Enemies = new Collider[16];
    protected Rigidbody m_Rigidbody;
    protected Targetable m_HomingTarget;

    protected virtual void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        GetComponent<Collider>().isTrigger = true;
    }

    public virtual void SetHomingTarget(Targetable target)
    {
        m_HomingTarget = target;
    }

    public  void FireInDirection(Vector3 startPoint, Vector3 fireVector)
    {
        transform.position = startPoint;
        var firingVector = fireVector.normalized * startSpeed;

        Fire(firingVector);
    }
    
    protected virtual void Fire(Vector3 firingVector)
    {
        transform.rotation = Quaternion.LookRotation(firingVector);
        m_Rigidbody.velocity = firingVector;

        fired?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Targetable>() == null)
        {
            return;
        }
        int number = Physics.OverlapSphereNonAlloc(transform.position, explodeRange, s_Enemies, explodeMask);
        for (int index = 0; index < number; index++)
        {
            Collider enemy = s_Enemies[index];
            var damageable = enemy.GetComponent<Targetable>();
            if (damageable == null)
            {
                continue;
            }

            damageable.TakeDamage(damager.damageAmout, damageable.position, damager.camp);
        }

        //播放特效
        if (explodeParticle)
        {
            var pfx = Instantiate(explodeParticle.gameObject);
            pfx.transform.position = transform.position;
            var ps = pfx.GetComponent<ParticleSystem>();
            ps.Play();
        }

        Destroy(gameObject);
    }
}