using UnityEngine;

public class Tower : MonoBehaviour
{
    public string towerName;
    public int MaxHP;
    public int HP;
    
    public GameObject ghostPrefab;
    public TowerLevel[] levels;
    public Vector2 Dimensions;
    public LayerMask enemyMask;
}