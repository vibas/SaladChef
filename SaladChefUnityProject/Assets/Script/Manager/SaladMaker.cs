using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Create salad / Adds vegetable to salad/ clear salad. 
/// This script must be attached to UI gridlayoutgroup element.
/// </summary>
public class SaladMaker : MonoBehaviour
{
    public GameObject choppedVegPrefab;
    Transform grid;
    List<GameObject> vegetableList;

    /// <summary>
    /// Adds item to salad. When player chops a new vegetable, that is added to salad
    /// </summary>
    /// <param name="vegetableSprite"></param>
    public void AddVegetable(Sprite vegetableSprite)
    {
        if(grid==null)
        {
            grid = transform;
        }
        if(vegetableList==null)
        {
            vegetableList = new List<GameObject>();
        }
        GameObject veg = Instantiate(choppedVegPrefab) as GameObject;
        veg.transform.SetParent(grid.transform);
        veg.transform.localScale = Vector3.one;
        veg.transform.localPosition = Vector3.zero;
        veg.GetComponent<Image>().sprite = vegetableSprite;
        vegetableList.Add(veg);
    }

    /// <summary>
    /// Create a salad from list of ingredient. When player takes salad from chop board, we use this
    /// </summary>
    /// <param name="saladIngredientList"></param>
    public void CreateSalad(List<string> saladIngredientList)
    {
        Sprite vegetableSprite = null;
        Inventory vegInventory = GameManager._instance.vegInventory;
        for (int i = 0; i < saladIngredientList.Count; i++)
        {            
            vegetableSprite = vegInventory.GetVegetable(saladIngredientList[i]).choppedItemSprite;
            AddVegetable(vegetableSprite);
        }
    }

    /// <summary>
    /// Clear salad list and remove all vegetables. 
    /// When player dumps salad/ takes salad from chop board/ delivers salad to customer, we use this
    /// </summary>
    public void ClearSalad()
    {
        if(vegetableList!=null && vegetableList.Count>0)
        {
            for (int i = vegetableList.Count - 1; i >= 0; i--)
            {
                Destroy(vegetableList[i]);
            }

            vegetableList.Clear();
            vegetableList = null;
        } 
    }
}
