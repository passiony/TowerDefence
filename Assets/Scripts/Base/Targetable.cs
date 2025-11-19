using System;
using UnityEngine;

public class Targetable : DamageableBehaviour
{
    public Transform targetTransform;

    protected override void Awake()
    {
        base.Awake();
        if (targetTransform == null)
        {
            targetTransform = transform;
        }
    }
    
    public  Vector3 position => targetTransform.position;
}
