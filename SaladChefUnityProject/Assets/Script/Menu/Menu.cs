using System.Collections.Generic;

/// <summary>
/// Holds all the salads created by SaladMenuManager
/// </summary>
public class Menu
{
    public Dictionary<int, Salad> menuDict;

    public Menu(List<Salad> allSalad)
    {
        menuDict = new Dictionary<int, Salad>();
        for (int i = 0; i < allSalad.Count; i++)
        {   
            menuDict.Add(allSalad[i].saladID, allSalad[i]);
        }       
    }	
}

[System.Serializable]
public struct Salad
{
    public int saladID;
    public int price;
    public List<string> ingredientsList;
}