using System;
using UnityEngine;

public class BallisticProjectile : Projectile
{
    public float startSpeed;
    public bool explode;
    public float explodeRange = 1.0f;
    public LayerMask explodeMask;
    public ParticleSystem explodeParticle;

    public event Action fired;

    private readonly Collider[] s_Enemies = new Collider[16];
    protected Rigidbody m_Rigidbody;
    protected Collider[] m_Colliders;
    protected virtual void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Colliders = GetComponentsInChildren<Collider>();
    }
    
    public void FireAtPoint(Vector3 startPoint, Vector3 targetPoint)
    {
        transform.position = startPoint;

        //计算从start到达target位置的子弹发射velocity
        Vector3 firingVector = (targetPoint - startPoint).normalized * startSpeed;
        Fire(firingVector);
    }

    protected virtual void Fire(Vector3 firingVector)
    {
        transform.rotation = Quaternion.LookRotation(firingVector);
        m_Rigidbody.velocity = firingVector;

        fired?.Invoke();
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (explode)
        {
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