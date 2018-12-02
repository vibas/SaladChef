using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config", order = 2)]
public class GameConfiguration : ScriptableObject
{
    [Header("Player Common Property")]
    public int playerMaxHoldingCapacity;
    public int playerInitialTotalTimer;

    [Header("Vegetables")]
    public Vegetable[] vegetableArray;
    public int vegChoppingTimerFactor;

    [Header("Customer Details")]
    public int angryCustomerTimerMultiplier;
    public float waitTimerForNextCustomer;
    public float waitTimeBeforeLeaving;  
    
}
