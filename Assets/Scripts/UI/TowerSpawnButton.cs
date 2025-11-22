using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpawnButton : MonoBehaviour
{
    public Image towerIcon;
    public Button buyButton;
    public Image energyIcon;
    public TextMeshProUGUI enegyText;
    public Color energyDefaultColor;
    public Color energyInvalidColor;

    private Tower m_Tower;
    public event Action<Tower> buttonTapped;
    public event Action<Tower> draggedOff;

    private void Awake()
    {
        buyButton.onClick.AddListener(OnButtonClick);
        LevelManager.Instance.currency.currencyChanged += UpdateButton;
    }

    private void OnButtonClick()
    {
        if (buttonTapped != null)
        {
            buttonTapped(m_Tower);
        }
    }

    public void InitButton(Tower towerData)
    {
        m_Tower = towerData;
        var level = towerData.levels[0];
        towerIcon.sprite = level.levelData.icon;
        enegyText.text = level.levelData.cost.ToString();
    }

    void UpdateButton()
    {
        var currency = LevelManager.Instance.currency;
        // Enable button
        if (currency.CanAfford(m_Tower.purchaseCost))
        {
            energyIcon.color = energyDefaultColor;
        }
        else if (!currency.CanAfford(m_Tower.purchaseCost))
        {
            energyIcon.color = energyInvalidColor;
        }
    }
}