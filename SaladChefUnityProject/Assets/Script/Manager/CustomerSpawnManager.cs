using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnManager : MonoBehaviour
{
    public CustomerCounter[] customerSpawningPoints;
    public GameObject customerPrefab;

    private void Start()
    {
        for (int i = 0; i < customerSpawningPoints.Length; i++)
        {
            GameObject customer = Instantiate(customerPrefab) as GameObject;
            customer.name = "Customer" + i;
            customer.transform.SetParent(customerSpawningPoints[i].transform);
            customer.transform.localScale = Vector3.one;
            customer.transform.localPosition = Vector3.zero;
            customerSpawningPoints[i].isOccupied = true;
            customerSpawningPoints[i].gameObject.SetActive(true);
            customerSpawningPoints[i].customer = customer.GetComponent<Customer>();
        }
    }

    public void InitiateAllCustomer()
    {
        for (int i = 0; i < customerSpawningPoints.Length; i++)
        {
            customerSpawningPoints[i].customer.CheckForMenu();
        }
    }

    // TODO time based Spawning
}
