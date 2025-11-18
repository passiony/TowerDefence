    using UnityEngine;

    /// <summary>
    /// 直接命中发射器
    /// </summary>
    public class HitscanLauncher:Launcher
    {
        public override void Launch(Targetable enemy, GameObject attack, Transform firingPoint)
        {
            // Vector3 startPosition = firingPoint.position;
            // var hitscanProjectile = attack.GetComponent<HitscanProjectile>();
            // if (hitscanProjectile == null)
            // {
            //     Debug.LogError("No hitscan projectile attached to projectile");
            //     DestroyImmediate(attack);
            //     return;
            // }
            // hitscanProjectile.FireAtTarget(startPosition, enemy);
            // hitscanProjectile.IgnoreCollision(LevelManager.instance.environmentColliders);
            // PlayParticles(fireParticleSystem, startPosition, enemy.position);
        }
    }
