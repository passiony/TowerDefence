using System;
using UnityEngine;

/// <summary>
/// 可受伤物体
/// </summary>
public class DamageableBehaviour : MonoBehaviour
{
    public Damageable configuration;

    public event Action<HitInfo> hit;

    public event Action<DamageableBehaviour> removed;

    public event Action<DamageableBehaviour> died;
}