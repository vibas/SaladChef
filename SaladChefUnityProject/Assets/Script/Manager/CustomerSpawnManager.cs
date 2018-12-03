using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnManager : MonoBehaviour
{
    public CustomerCounter[] customerSpawningPoints;
    public GameObject customerPrefab;
    float waitTimerForNextCustomer, timer;
    bool shouldTimerRun = false;
    List<Customer> allCustomer;

    private void Start()
    {
        waitTimerForNextCustomer = GameManager._instance.gameConfig.waitTimerForNextCustomer;
        timer = 0;        
    }

    public void SpawnCustomer()
    {
        if (allCustomer == null)
            allCustomer = new List<Customer>();

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

            if (!allCustomer.Contains(customer.GetComponent<Customer>()))
            {
                allCustomer.Add(customer.GetComponent<Customer>());
            }
        }
    }

    public void RemoveCustomerFromAllCustomer(Customer customer)
    {
        if (allCustomer.Contains(customer))
        {
            allCustomer.Remove(customer);
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
        if (shouldTimerRun)
        {
            timer += Time.deltaTime;
            if (timer > waitTimerForNextCustomer)
            {
                SpawnCustomer();
                timer = 0;
            }
        }
    }

    public void StartWaveTimer()
    {
        shouldTimerRun = true;
    }

    public void StopWaveTimer()
    {
        shouldTimerRun = false;
    }

    public void ResetWaveTimer()
    {
        shouldTimerRun = false;
        timer = 0;
    }

    public void StartCustomerTimer()
    {
        for (int i = 0; i < allCustomer.Count; i++)
        {
            allCustomer[i].customerStateMachine.shouldRunTimer = true;
        }
    }

    public void StopCustomerTimer()
    {
        for (int i = 0; i < allCustomer.Count; i++)
        {
            allCustomer[i].customerStateMachine.shouldRunTimer = false;
        }
    }

    public void ResetAllCustomer()
    {
        if(allCustomer!=null)
        {
            for (int i = allCustomer.Count - 1; i >= 0; i--)
            {
                allCustomer[i].counter.isOccupied = false;
                Destroy(allCustomer[i].gameObject);
            }
            allCustomer.Clear();
            allCustomer = null;
        }        
    }
}
