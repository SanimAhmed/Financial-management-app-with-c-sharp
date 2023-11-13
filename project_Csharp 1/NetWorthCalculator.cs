using System;
using System.Collections.Generic;

public class NetWorthCalculator
{
    private Dictionary<string, decimal> Assets;
    private Dictionary<string, decimal> Liabilities;

    public decimal TotalAssets => CalculateTotal(Assets);
    public decimal TotalLiabilities => CalculateTotal(Liabilities);
    public decimal NetWorth => TotalAssets - TotalLiabilities;

    public NetWorthCalculator()
    {
        Assets = new Dictionary<string, decimal>();
        Liabilities = new Dictionary<string, decimal>();
    }

    public void AddAsset(string assetName, decimal value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("Asset value must be positive.");
        }

        Assets[assetName] = value;
    }

    public void AddLiability(string liabilityName, decimal value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("Liability value must be positive.");
        }

        Liabilities[liabilityName] = value;
    }

    public void RemoveAsset(string assetName)
    {
        if (!Assets.Remove(assetName))
        {
            Console.WriteLine($"Asset '{assetName}' not found.");
        }
    }

    public void RemoveLiability(string liabilityName)
    {
        if (!Liabilities.Remove(liabilityName))
        {
            Console.WriteLine($"Liability '{liabilityName}' not found.");
        }
    }

    public void UpdateAssetValue(string assetName, decimal newValue)
    {
        if (!Assets.ContainsKey(assetName))
        {
            Console.WriteLine($"Asset '{assetName}' not found.");
            return;
        }

        Assets[assetName] = newValue;
    }

    public void UpdateLiabilityValue(string liabilityName, decimal newValue)
    {
        if (!Liabilities.ContainsKey(liabilityName))
        {
            Console.WriteLine($"Liability '{liabilityName}' not found.");
            return;
        }

        Liabilities[liabilityName] = newValue;
    }

    private decimal CalculateTotal(Dictionary<string, decimal> financialItems)
    {
        decimal total = 0;
        foreach (var item in financialItems)
        {
            total += item.Value;
        }
        return total;
    }

    public void DisplayNetWorthSummary()
    {
        Console.WriteLine($"Total Assets: {TotalAssets}");
        Console.WriteLine($"Total Liabilities: {TotalLiabilities}");
        Console.WriteLine($"Net Worth: {NetWorth}");
    }

    // Additional methods as needed for detailed tracking and analysis
}
