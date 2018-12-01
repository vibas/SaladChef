using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaladMeuManager : MonoBehaviour
{
    Menu menu;
    List<Salad> salads;
    public int maxNumberOfSalad;
    List<string> allVegList;

    public void CreateMenu()
    {
        allVegList = new List<string>(GameManager._instance.vegInventory.GetAllVegetables().Keys);
        salads = new List<Salad>();
        for (int i = 0; i < maxNumberOfSalad; i++)
        {
            Salad s = new Salad();
            s.saladID = i;
            s.ingredientsList = GetIngredientList(i);
            salads.Add(s);           
        }
        menu = new Menu(salads);       
    }

    // TODO Correct this method 
    List<string> GetIngredientList(int i)
    {
        string primaryItem = allVegList[i];
        List<string> ingredientList = new List<string>();
        ingredientList.Add(primaryItem);
        int counter = 0;
        for (int j = 0; j < allVegList.Count; j++)
        {
            if (j == i)
                continue;
           
            ingredientList.Add(allVegList[j]);
            counter++;

            if (counter == 2)
                break;
        }  
        return ingredientList;
    }

    public List<string> GetRandomSaladFromMenu()
    {
        int randomNumer = Random.Range(0, menu.menuDict.Count);
        return menu.menuDict[randomNumer];
    }
}
