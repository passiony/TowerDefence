using UnityEngine;

public class TowerPlacementGrid : MonoBehaviour
{
    public PlacementTile tilePrefab;
    public Vector2Int dimensions;

    void Awake()
    {
        SetupGrid();
    }

    public void SetupGrid()
    {
    }

    public Vector2Int WorldToGrid()
    {
        return Vector2Int.zero;
    }

    public Vector3 GridToWorld()
    {
        return Vector3.zero;
    }

    public void Fits()
    {
        
    }
    public void Occupy()
    {
    }

    public void Clear()
    {
    }
}