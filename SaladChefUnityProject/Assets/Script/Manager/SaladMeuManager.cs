﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Attached to GameManager Game Object
///  Handles creating all possible menu from the vegetable list
///  Customer gets random item from Menu using this class
/// </summary>
public class SaladMeuManager : MonoBehaviour
{
    Menu menu;
    List<Salad> salads;    
    List<string> allVegList;
    List<List<string>> allCominationList;
    List<List<string>> allCombination2, allCombination3;

    public void CreateMenu()
    {
        allVegList = new List<string>(GameManager._instance.vegInventory.GetAllVegetables().Keys);
        salads = new List<Salad>();        

        allCombination2 = GetAllCombination(allVegList,2);
        allCombination3 = GetAllCombination(allVegList, 3);

        allCominationList = new List<List<string>>();
        allCominationList.AddRange(allCombination2);
        allCominationList.AddRange(allCombination3);

        int count = 0;
        for (int i = 0; i < allCominationList.Count; i++)
        {             
            Salad s = new Salad();
            s.saladID = count;           
            s.ingredientsList = allCominationList[i];
            s.price = s.ingredientsList.Count * 5;
            salads.Add(s);
            count++;
        }
        
        menu = new Menu(salads);       
    }

    List<List<string>> GetAllCombination(List<string> sourceList, int l)
    {
        List<List<string>> list = new List<List<string>>();
        for (int i = 0; i < sourceList.Count; i++)
        {
            for (int j = 0; j < sourceList.Count; j++)
            {
                List<string> internalList = new List<string>();
                if (j <= i)
                {
                    continue;
                }
                if (l == 2)
                {
                    internalList.Add(sourceList[i]);
                    internalList.Add(sourceList[j]);
                    list.Add(internalList);
                }
                if (l == 3)
                {

                    for (int k = 0; k < sourceList.Count; k++)
                    {
                        internalList = new List<string>();
                        if (k <= j)
                        {
                            continue;
                        }
                        internalList.Add(sourceList[i]);
                        internalList.Add(sourceList[j]);
                        internalList.Add(sourceList[k]);
                        list.Add(internalList);
                    }
                }
            }
        }
        return list;
    }

    public Salad GetRandomSaladFromMenu()
    {
        int randomNumer = Random.Range(0, menu.menuDict.Count);
        return menu.menuDict[randomNumer];
    }
}
