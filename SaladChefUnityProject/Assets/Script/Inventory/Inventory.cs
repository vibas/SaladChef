using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Vegetable GetVegetable(string itemID)
    {
        Vegetable veg = new Vegetable();
        if(vegetableInventory.ContainsKey(itemID))
        {
            veg = vegetableInventory[itemID];
        }
        return veg;
    }

    public Dictionary<string, Vegetable> GetAllVegetables()
    {
        return vegetableInventory;
    }
}
