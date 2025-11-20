using System;
using UnityEngine;

public class Tower : Targetable
{
    public string towerName;
    public Vector2Int dimensions;
    public TowerPlacementGhost towerGhostPrefab;
    public TowerLevel[] levels;
    public LayerMask enemyLayerMask;
    
    public Vector2Int gridPosition { get; private set; }
    public IPlacementArea placementArea { get; private set; }
    public int currentLevel{ get; protected set; }
    public TowerLevel currentTowerLevel{ get; protected set; }
    public int purchaseCost => levels[0].levelData.cost;

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
    
    void SetLevel(int level)
    {
        if (level < 0 || level >= levels.Length)
        {
            return;
        }
        currentLevel = level;
        if (currentTowerLevel != null)
        {
            Destroy(currentTowerLevel.gameObject);
        }
        // instantiate the visual representation
        currentTowerLevel = Instantiate(levels[currentLevel], transform);
        currentTowerLevel.Initialize(this, enemyLayerMask, configuration.camp);

        ScaleHealth();
    }

    private void ScaleHealth()
    {
        configuration.SetMaxHealth(currentTowerLevel.levelData.maxHealth);
			
        //恢复当前血条比例
        // int currentHealth = Mathf.FloorToInt(configuration.normalisedHealth * currentTowerLevel.levelData.maxHealth);
        // configuration.SetHealth(currentHealth);
    }

}