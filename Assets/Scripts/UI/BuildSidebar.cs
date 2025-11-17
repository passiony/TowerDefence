using System;
using UnityEngine;

public class BuildSidebar : MonoBehaviour
{
    public TowerSpawnButton towerSpawnButton;

    public void Start()
    {
        foreach (Tower tower in LevelManager.Instance.towerLibrary)
        {
            TowerSpawnButton button = Instantiate(towerSpawnButton, transform);
            button.InitButton(tower);
            button.buttonTapped += OnButtonTapped;
        }
    }

    private void OnButtonTapped(Tower tower)
    {
        var gameUI = GameUI.Instance;
        if (gameUI.isBuilding)
        {
            gameUI.CancelGhostPlacement();
        }

        gameUI.SetToBuildMode(tower);
    }
}