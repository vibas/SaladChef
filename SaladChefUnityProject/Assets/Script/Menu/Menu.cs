using System.Collections.Generic;
public class Menu
{
    public Dictionary<int, List<string>> menuDict;

    public Menu(List<Salad> allSalad)
    {
        menuDict = new Dictionary<int, List<string>>();
        for (int i = 0; i < allSalad.Count; i++)
        {   
            menuDict.Add(allSalad[i].saladID, allSalad[i].ingredientsList);
        }       
    }	
}

[System.Serializable]
public struct Salad
{
    public int saladID;
    public List<string> ingredientsList;
}