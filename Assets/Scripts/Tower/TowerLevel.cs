using System;
using UnityEngine;

[Serializable]
public class LevelData
{
    public int cost;
    public int sell;
    public int maxHealth;
}

public class TowerLevel : MonoBehaviour
{
    public GameObject buildEffect;
    public LevelData levelData;
}