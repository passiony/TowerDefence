using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public string towerName;
    public int MaxHP;
    public int HP;
    public int level;

    public Vector2Int dimensions;
    public TowerPlacementGhost towerGhostPrefab;
    public TowerLevel[] levels;
    public Vector2Int Dimensions;
    public LayerMask enemyMask;

    public Vector2Int gridPosition { get; private set; }
    public IPlacementArea placementArea { get; private set; }

    public int purchaseCost
    {
        get { return levels[0].levelData.cost; }
    }

    private void Start()
    {
    }

    void SetLevel(int level)
    {
    }

    public void Initialize(TowerPlacementGrid targetArea, Vector2Int gridPos)
    {
        placementArea = targetArea;
        gridPosition = gridPos;

        if (targetArea != null)
        {
            transform.position = placementArea.GridToWorld(gridPos, dimensions);
            transform.rotation = placementArea.transform.rotation;
            targetArea.Occupy(gridPos, dimensions);
        }

        SetLevel(0);
    }
}