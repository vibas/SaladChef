using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inventory Class holds all vegetable data
/// </summary>
public class Inventory
{
    Dictionary<string, Vegetable> vegetableInventory;
	
    public Inventory(Vegetable[] veg)
    {
        vegetableInventory = new Dictionary<string, Vegetable>();

        for (int i = 0; i < veg.Length; i++)
        {
            vegetableInventory.Add(veg[i].itemID, veg[i]);
        }
    }

    /// <summary>
    /// Get one vegetable with given ID
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns></returns>
    public Vegetable GetVegetable(string itemID)
    {
        Vegetable veg = new Vegetable();
        if(vegetableInventory.ContainsKey(itemID))
        {
            veg = vegetableInventory[itemID];
        }
        return veg;
    }

    /// <summary>
    /// Get all vegetable Dictionary
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, Vegetable> GetAllVegetables()
    {
        return vegetableInventory;
    }
}

[System.Serializable]
public struct Vegetable
{
    public string itemID;
    public Sprite itemSprite;
    public Sprite choppedItemSprite;
}

