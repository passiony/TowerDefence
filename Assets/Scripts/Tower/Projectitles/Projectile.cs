using System;
using UnityEngine;

public abstract class Projectile :MonoBehaviour
{
    public Damager damager;

    event Action fired;
}