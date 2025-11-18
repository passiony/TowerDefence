using UnityEngine;

public struct HealthChangeInfo
{
    public Damageable damageable;

    public float oldHealth;

    public float newHealth;

    public ECamp damageCamp;

    public float healthDifference
    {
        get { return newHealth - oldHealth; }
    }

    public float absHealthDifference
    {
        get { return Mathf.Abs(healthDifference); }
    }
}