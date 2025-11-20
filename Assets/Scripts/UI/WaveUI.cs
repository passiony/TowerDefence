using System;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleFrame.Tool.UI
{
    public class WaveUI : MonoBehaviour
    {
        public Text waveTxt;
        public Image waveProgress;

        public void OnEnable()
        {
            LevelManager.Instance.waveManager.waveChanged += OnWaveChanged;
        }

        private void OnDisable()
        {
            LevelManager.Instance.waveManager.waveChanged -= OnWaveChanged;
        }

        private void OnWaveChanged()
        {
            int currentWave = LevelManager.Instance.waveManager.waveNumber;
            int totalWaves = LevelManager.Instance.waveManager.totalWaves;
            string output = string.Format("{0}/{1}", currentWave, totalWaves);
            waveTxt.text = output;
            waveProgress.fillAmount = LevelManager.Instance.waveManager.waveProgress;
        }
    }
}