using System.Collections.Generic;
using UnityEngine;

public interface ILauncher
{
    void Launch(Targetable enemy, GameObject attack, Transform firingPoint);

    /// <summary>
    /// 多发射口
    /// </summary>
    void Launch(Targetable enemy, GameObject attack, Transform[] firingPoints);

    /// <summary>
    /// 对多个敌人射击
    /// </summary>
    void Launch(List<Targetable> enemies, GameObject attack, Transform[] firingPoints);
}