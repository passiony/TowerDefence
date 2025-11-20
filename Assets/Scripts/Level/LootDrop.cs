using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    public int dropAmount;
    DamageableBehaviour damageable;
    
    void Awake()
    {
        damageable = gameObject.GetComponent<DamageableBehaviour>();
    }

    private void OnEnable()
    {
        damageable.configuration.died += onDied;
    }

    private void OnDisable()
    {
        damageable.configuration.died -= onDied;
    }

    private void onDied(HealthChangeInfo obj)
    {
        LevelManager.Instance.currency.AddCurrency(dropAmount); 
    }

}
