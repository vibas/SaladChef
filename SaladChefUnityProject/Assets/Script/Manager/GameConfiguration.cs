using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config", order = 2)]
public class GameConfiguration : ScriptableObject
{
    [Header("Player Common Property")]
    public int playerMaxHoldingCapacity;

    [Header("Vegetables")]
    public Vegetable[] vegetableArray;
}
