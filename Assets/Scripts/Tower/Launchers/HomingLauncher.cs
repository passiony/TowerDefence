using UnityEngine;

/// <summary>
/// 追踪弹
/// </summary>
public class HomingLauncher : Launcher
{
    [Range(-90, 90)] public float firingAngle;

    public override void Launch(Targetable enemy, GameObject attack, Transform firingPoint)
    {
        var homingMissile = attack.GetComponent<WobblingHomingProjectile>();
        if (homingMissile == null)
        {
            Debug.LogError("No HomingLinearProjectile attached to attack object");
            return;
        }

        Vector3 startingPoint = firingPoint.position;
        
        var attackAffector = GetComponent<AttackAffector>();
        Vector3 direction = attackAffector.towerTargetter.transform.forward;

        Vector3 binormal = Vector3.Cross(direction, Vector3.up);
        Quaternion rotation = Quaternion.AngleAxis(45, binormal);

        Vector3 adjustedFireVector = rotation * direction;

        homingMissile.SetHomingTarget(enemy);
        homingMissile.FireInDirection(startingPoint, adjustedFireVector);
    }
}