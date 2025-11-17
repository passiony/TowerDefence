using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    public Currency currency { get; protected set; }
    public int startingCurrency;

    public Tower[] towerLibrary;
    
    public override void Init()
    {
        base.Init();
        currency = new Currency(startingCurrency);
    }
}