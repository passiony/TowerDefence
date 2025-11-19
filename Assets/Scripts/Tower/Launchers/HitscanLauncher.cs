    using UnityEngine;

    /// <summary>
    /// 直接命中发射器
    /// </summary>
    public class HitscanLauncher:Launcher
    {
        public ParticleSystem fireParticleSystem;
        
        public override void Launch(Targetable enemy, GameObject attack, Transform firingPoint)
        {
            Vector3 startPosition = firingPoint.position;
            var hitscanProjectile = attack.GetComponent<HItscanProjectitle>();
            if (hitscanProjectile == null)
            {
                Debug.LogError("No hitscan projectile attached to projectile");
                DestroyImmediate(attack);
                return;
            }
            hitscanProjectile.FireAtTarget(startPosition, enemy);
            fireParticleSystem.Play();
        }
    }
