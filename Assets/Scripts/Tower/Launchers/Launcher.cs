using System.Collections.Generic;
using UnityEngine;

public abstract class Launcher : MonoBehaviour, ILauncher
{
    public abstract void Launch(Targetable enemy, GameObject attack, Transform firingPoint);

    public virtual void Launch(List<Targetable> enemies, GameObject attack, Transform[] firingPoints)
    {
        int enemyIndex = 0;
        int enemyCount = enemies.Count;
        int currentFiringPointIndex = 0;
        int firingPointLength = firingPoints.Length;
        for (int i = 0; i < firingPointLength; i++)
        {
            Targetable enemy = enemies[enemyIndex];
            enemyIndex = (enemyIndex + 1) % enemyCount;
            Transform firingPoint = firingPoints[currentFiringPointIndex];
            currentFiringPointIndex = (currentFiringPointIndex + 1) % firingPointLength;

            var bullet = Instantiate(attack);
            bullet.transform.position = firingPoint.position;
            Launch(enemy, bullet, firingPoint);
        }
    }

    public virtual void Launch(Targetable enemy, GameObject attack, Transform[] firingPoints)
    {
        var bullet = Instantiate(attack);

        int index = Random.Range(0, firingPoints.Length);
        Launch(enemy, bullet, firingPoints[index]);
    }
}