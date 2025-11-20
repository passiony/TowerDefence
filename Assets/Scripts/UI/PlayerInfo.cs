using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleFrame.Tool.UI
{
    public class PlayerInfo : MonoBehaviour
    {
        public Text lifeTxt;
        public Text moneyTxt;

        private void Start()
        {
            LevelManager.Instance.playerHome.configuration.healthChanged += OnHealthChagne;
            LevelManager.Instance.currency.currencyChanged += OnCurrencyChanged;
        }

        private void OnHealthChagne(HealthChangeInfo obj)
        {
            lifeTxt.text = obj.newHealth.ToString("00");
        }

        private void OnCurrencyChanged()
        {
            moneyTxt.text = LevelManager.Instance.currency.moneyAmout.ToString();
        }
    }
}