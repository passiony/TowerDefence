using System;
using UnityEngine;

public class TowerPlacementGrid : MonoBehaviour, IPlacementArea
{
    public PlacementTile tilePrefab;
    public Vector2Int dimensions;
    public float gridSize = 1;

    bool[,] m_AvailableCells;
    PlacementTile[,] m_Tiles;

    void Awake()
    {
        SetupGrid();
    }

    public void SetupGrid()
    {
        m_AvailableCells = new bool[dimensions.x, dimensions.y];
        m_Tiles = new PlacementTile[dimensions.x, dimensions.y];

        var tileParent = new GameObject("Container");
        tileParent.transform.parent = transform;
        tileParent.transform.localPosition = Vector3.zero;
        tileParent.transform.localRotation = Quaternion.identity;

        for (int y = 0; y < dimensions.y; y++)
        {
            for (int x = 0; x < dimensions.x; x++)
            {
                Vector3 targetPos = GridToWorld(new Vector2Int(x, y), new Vector2Int(1, 1));
                targetPos.y += 0.01f;
                PlacementTile newTile = Instantiate(tilePrefab);
                newTile.transform.parent = tileParent.transform;
                newTile.transform.position = targetPos;
                newTile.transform.localRotation = Quaternion.identity;

                m_Tiles[x, y] = newTile;
                newTile.SetState(false);
            }
        }
    }

    public Vector2Int WorldToGrid(Vector3 worldPosition, Vector2Int size)
    {
        Vector3 location = transform.InverseTransformPoint(worldPosition);
        location /= gridSize;
        //炮塔尺寸一半
        var offset = new Vector3(size.x * 0.5f, 0, size.y * 0.5f);
        //计算原点（左上角）
        location -= offset;
        int xPos = Mathf.RoundToInt(location.x);
        int yPos = Mathf.RoundToInt(location.z);

        return new Vector2Int(xPos, yPos);
    }

    public Vector3 GridToWorld(Vector2Int gridPosition, Vector2Int size)
    {
        Vector3 localPos = new Vector3(gridPosition.x + (size.x * 0.5f), 0, gridPosition.y + (size.y * 0.5f)) *
                           gridSize;
        return transform.TransformPoint(localPos);
    }

    public bool Fits(Vector2Int gridPos, Vector2Int size)
    {
        //尺寸大于边界
        if (size.x > dimensions.x || size.y > dimensions.y)
        {
            return false;
        }

        //位置大于边界
        var extents = gridPos + size;
        if ((gridPos.x < 0) || (gridPos.y < 0) ||
            (extents.x > dimensions.x) || (extents.y > dimensions.y))
        {
            return false;
        }

        //格子是否已被占用
        for (int y = gridPos.y; y < extents.y; y++)
        {
            for (int x = gridPos.x; x < extents.x; x++)
            {
                if (m_AvailableCells[x, y])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void Occupy(Vector2Int gridPos, Vector2Int size)
    {
        Vector2Int extents = gridPos + size;
        
        // Fill those positions
        for (int y = gridPos.y; y < extents.y; y++)
        {
            for (int x = gridPos.x; x < extents.x; x++)
            {
                m_AvailableCells[x, y] = true;
                if (m_Tiles != null && m_Tiles[x, y] != null)
                {
                    m_Tiles[x, y].SetState(true);
                }
            }
        }
    }

    public void Clear(Vector2Int gridPos, Vector2Int size)
    {
        Vector2Int extents = gridPos + size;
        
        // Fill those positions
        for (int y = gridPos.y; y < extents.y; y++)
        {
            for (int x = gridPos.x; x < extents.x; x++)
            {
                m_AvailableCells[x, y] = false;
                if (m_Tiles != null && m_Tiles[x, y] != null)
                {
                    m_Tiles[x, y].SetState(false);
                }
            }
        }
    }

    void OnValidate()
    {
        // Validate grid size
        if (gridSize <= 0)
        {
            Debug.LogError("Negative or zero grid size is invalid");
            gridSize = 1;
        }

        // Validate dimensions
        if (dimensions.x <= 0 ||
            dimensions.y <= 0)
        {
            Debug.LogError("Negative or zero grid dimensions are invalid");
            dimensions = new Vector2Int(Mathf.Max(dimensions.x, 1), Mathf.Max(dimensions.y, 1));
        }

        // Ensure collider is the correct size
        ResizeCollider();
    }

    void ResizeCollider()
    {
        var myCollider = GetComponent<BoxCollider>();
        Vector3 size = new Vector3(dimensions.x, 0, dimensions.y) * gridSize;
        myCollider.size = size;
        myCollider.center = size * 0.5f;
    }

    private void OnDrawGizmos()
    {
        GetComponent<BoxCollider>().hideFlags = HideFlags.None;
        Color prevColor = Gizmos.color;
        Matrix4x4 originalMatrix = Gizmos.matrix;

        Gizmos.color = Color.cyan;
        Gizmos.matrix = transform.localToWorldMatrix;

        for (int y = 0; y < dimensions.y; y++)
        {
            for (int x = 0; x < dimensions.x; x++)
            {
                var position = new Vector3((x + 0.5f) * gridSize, 0, (y + 0.5f) * gridSize);
                Gizmos.DrawWireCube(position, new Vector3(gridSize, 0, gridSize));
            }
        }

        Gizmos.color = prevColor;
        Gizmos.matrix = originalMatrix;
    }
}