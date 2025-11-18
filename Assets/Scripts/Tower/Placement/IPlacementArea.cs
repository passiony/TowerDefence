using UnityEngine;

public interface IPlacementArea
{
    Transform transform { get; }

    Vector2Int WorldToGrid(Vector3 worldPosition, Vector2Int sizeOffset);

    Vector3 GridToWorld(Vector2Int gridPosition, Vector2Int sizeOffset);

    bool Fits(Vector2Int gridPos, Vector2Int size);
    
    void Occupy(Vector2Int gridPos, Vector2Int size);

    void Clear(Vector2Int gridPos, Vector2Int size);
}