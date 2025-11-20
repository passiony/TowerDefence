using SimpleFrame.Tool.Level;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    public Currency currency { get; protected set; }
    public WaveManager waveManager { get; protected set; }
    
    public int startingCurrency;
    public PlayerHome playerHome;
    public Tower[] towerLibrary;

    public override void Init()
    {
        base.Init();
        Application.targetFrameRate = 60;
        currency = new Currency(startingCurrency);
        waveManager = GetComponentInChildren<WaveManager>();
    }
}