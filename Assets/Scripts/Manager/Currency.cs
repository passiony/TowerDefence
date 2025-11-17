using System;

public class Currency
{
    public int currentCurrency { get; private set; }
    
    public event Action currencyChanged;

    public Currency(int startingCurrency)
    {
        ChangeCurrency(startingCurrency);
    }
    
    public void AddCurrency(int increment)
    {
        ChangeCurrency(increment);
    }

    public bool CanAfford(int cost)
    {
        return currentCurrency >= cost;
    }

    public bool TryPurchase(int cost)
    {
        if (!CanAfford(cost))
        {
            return false;
        }

        ChangeCurrency(cost);
        return true;
    }

    private void ChangeCurrency(int increment)
    {
        currentCurrency += increment;
        currencyChanged?.Invoke();
    }
}