using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnManager : MonoBehaviour
{
    public CustomerCounter[] customerSpawningPoints;
    public GameObject customerPrefab;
    float waitTimerForNextCustomer,timer;
    bool shouldTimerRun = false;

    private void Start()
    {
        waitTimerForNextCustomer = GameManager._instance.gameConfig.waitTimerForNextCustomer;
        timer = 0;
    }

    public void SpawnCustomer()
    {
        CustomerCounter freeCounter = GetFreeCustomerCounter();

        if (freeCounter)
        {
            GameObject customer = Instantiate(customerPrefab) as GameObject;
            customer.name = "Customer";
            customer.transform.SetParent(freeCounter.transform);
            customer.transform.localScale = Vector3.one;
            customer.transform.localPosition = Vector3.zero;
            freeCounter.isOccupied = true;
            freeCounter.gameObject.SetActive(true);
            freeCounter.customer = customer.GetComponent<Customer>();
            freeCounter.customer.counter = freeCounter;
            freeCounter.customer.CheckForMenu();
        }        
    }

    CustomerCounter GetFreeCustomerCounter()
    {
        CustomerCounter freeCounter = null;
        for (int i = 0; i < customerSpawningPoints.Length; i++)
        {
            if (!customerSpawningPoints[i].isOccupied)
            {
                freeCounter = customerSpawningPoints[i];
                break;
            }
        }
        return freeCounter;
    }

    private void Update()
    {
        if(shouldTimerRun)
        {
            timer += Time.deltaTime;
            if (timer > waitTimerForNextCustomer)
            {
                SpawnCustomer();
                timer = 0;
            }
        }        
    }

    public void StartTImer()
    {
        shouldTimerRun = true;
    }

    public void StopTimer()
    {
        shouldTimerRun = false;
    }
}
