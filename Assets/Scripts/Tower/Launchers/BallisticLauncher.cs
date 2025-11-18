using UnityEngine;

/// <summary>
/// 投射物 发射器
/// </summary>
public class BallisticLauncher : Launcher
{
    /// <summary>
    /// 用于提供发射反馈的粒子系统
    /// </summary>
    public ParticleSystem fireParticleSystem;

    /// <summary>
    /// 从单个发射点向单个敌人发射单个投射物
    /// </summary>
    public override void Launch(Targetable enemy, GameObject projectile, Transform firingPoint)
    {
        Vector3 startPosition = firingPoint.position;
        var ballisticProjectile = projectile.GetComponent<BallisticProjectile>();
        if (ballisticProjectile == null)
        {
            Debug.LogError("No ballistic projectile attached to projectile");
            DestroyImmediate(projectile);
            return;
        }
        ballisticProjectile.FireAtPoint(startPosition, enemy.position);
    }
}