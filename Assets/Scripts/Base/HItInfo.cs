using UnityEngine;

public struct HitInfo
{
    public HealthChangeInfo HealthChangeInfo { get; }
    public Vector3 DamagePoint { get; }

    public HitInfo(HealthChangeInfo info, Vector3 damageLocation)
    {
        DamagePoint = damageLocation;
        HealthChangeInfo = info;
    }
}