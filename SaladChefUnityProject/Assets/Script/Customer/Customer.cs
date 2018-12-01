using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public List<string> orderSalad;

    public GameObject itemPrefab;
    public Transform content;

    public CustomerCounter counter;

    public void CheckForMenu()
    {
        orderSalad = GameManager._instance.saladMeuManager.GetRandomSaladFromMenu();
        DisplayOrderItemIngredients();
    }

    void DisplayOrderItemIngredients()
    {
        for (int i = 0; i < orderSalad.Count; i++)
        {
            GameObject vegItem = Instantiate(itemPrefab) as GameObject;
            vegItem.transform.SetParent(content);
            vegItem.transform.localScale = Vector3.one;
            vegItem.transform.localPosition = Vector3.zero;
            vegItem.GetComponent<Image>().sprite = GameManager._instance.vegInventory.GetVegetable(orderSalad[i]).itemSprite;
        }
    }
}


