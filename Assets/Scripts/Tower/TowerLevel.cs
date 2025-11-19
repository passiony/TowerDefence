using System;
using UnityEngine;

[Serializable]
public class LevelData
{
    public int cost;
    public int sell;
    public int maxHealth;
    public Sprite icon;
}

public class TowerLevel : MonoBehaviour
{
    public GameObject buildEffect;
    public LevelData levelData;

    Affector[] m_Affectors;

    public void Initialize(Tower tower, LayerMask enemyLayerMask, ECamp camp)
    {
        m_Affectors = GetComponentsInChildren<Affector>();
        foreach (Affector effect in m_Affectors)
        {
            effect.Initialize(camp, enemyLayerMask);
        }
    }
}