using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public List<string> orderSalad;
	public void CheckForMenu()
    {
        orderSalad = GameManager._instance.saladMeuManager.GetRandomSaladFromMenu();
    }
}


