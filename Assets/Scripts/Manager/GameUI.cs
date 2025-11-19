using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameUI : MonoSingleton<GameUI>
{
    private TowerPlacementGhost m_CurrentTower;

    public bool m_IsFitArea;
    public bool isBuilding;
    private TowerPlacementGrid m_CurrentArea;
    private Vector2Int m_GridPosition;

    public void CancelGhostPlacement()
    {
        // if (buildInfoUI != null)
        // {
        //     buildInfoUI.Hide();
        // }
        Destroy(m_CurrentTower.gameObject);
        m_CurrentTower = null;
    }

    public void SetToBuildMode(Tower towerToBuild)
    {
        if (m_CurrentTower != null)
        {
            // Destroy current ghost
            CancelGhostPlacement();
        }

        isBuilding = true;
        m_CurrentTower = Instantiate(towerToBuild.towerGhostPrefab);
        m_CurrentTower.Initialize(towerToBuild);
    }

    private void Update()
    {
        if (isBuilding)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = LayerMask.GetMask("PlacementLayer", "Default");
            if (Physics.Raycast(ray, out RaycastHit hit, 100, layerMask))
            {
                m_CurrentTower.transform.position = hit.point;

                m_CurrentArea = hit.collider.GetComponent<TowerPlacementGrid>();
                if (m_CurrentArea != null)
                {
                    var dimensions = m_CurrentTower.controller.dimensions;
                    m_GridPosition = m_CurrentArea.WorldToGrid(hit.point, dimensions);
                    m_IsFitArea = m_CurrentArea.Fits(m_GridPosition, dimensions);
                    m_CurrentTower.Move(m_CurrentArea.GridToWorld(m_GridPosition, dimensions),
                        m_CurrentArea.transform.rotation, m_IsFitArea);
                    
                    //点击防止炮塔
                    if (Input.GetMouseButtonDown(0))
                    {
                        TryPlaceTower();
                    }
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void TryPlaceTower()
    {
        if (m_CurrentTower == null)
        {
            return;
        }

        if (!m_IsFitArea)
        {
            return;
        }

        isBuilding = false;
        int cost = m_CurrentTower.controller.purchaseCost;
        bool successfulPurchase = LevelManager.Instance.currency.TryPurchase(cost);
        if (successfulPurchase)
        {
            Tower controller = m_CurrentTower.controller;
            Tower createdTower = Instantiate(controller);
            createdTower.Initialize(m_CurrentArea, m_GridPosition);
            CancelGhostPlacement();
        }
    }
}