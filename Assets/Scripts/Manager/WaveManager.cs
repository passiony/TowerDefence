using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public NodePath m_NodePath;

    public Wave[] waves;

    private int m_Index;

    public int waveNumber => m_Index + 1;
    public int totalWaves => waves.Length;
    public float waveProgress
    {
        get
        {
            if (waves == null || waves.Length <= m_Index)
            {
                return 0;
            }

            return waves[m_Index].progress;
        }
    }

    public event Action waveChanged;
    public event Action spawningCompleted;

    private void Start()
    {
        m_Index = 0;
        StartNext();
    }

    public void StartNext()
    {
        if (m_Index < waves.Length)
        {
            SpawnWave();
        }
    }

    protected virtual void SpawnWave()
    {
        var wave = waves[m_Index];
        wave.StartWave(m_NodePath);
        wave.completed += OnWaveCompleted;
        m_Index++;
    }

    private void OnWaveCompleted()
    {
        StartNext();
    }
}