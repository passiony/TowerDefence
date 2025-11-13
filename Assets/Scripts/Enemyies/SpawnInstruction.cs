using System;
using UnityEngine;

[Serializable]
public class SpawnInstruction
{
    public GameObject spawnPrefab;
    public float delay;
    public int startNode;
}