using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config", order = 2)]
public class GameConfiguration : ScriptableObject
{
    [Header("Player Common Property")]
    public int playerMaxHoldingCapacity;

    [Header("Vegetables")]
    public Vegetable[] vegetableArray;    

    [Header("Customer Details")]
    public int angryCustomerTimerMultiplier;
    public float waitTimerForNextCustomer;
    public float waitTimeBeforeLeaving;
}
