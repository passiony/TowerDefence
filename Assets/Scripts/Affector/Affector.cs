using UnityEngine;

public abstract class Affector : MonoBehaviour
{
    public ECamp camp { get; protected set; }
    public LayerMask enemyMask { get; protected set; }

    public virtual void Initialize(ECamp affectorCamp, LayerMask mask)
    {
        camp = affectorCamp;
        enemyMask = mask;
    }
}