using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpConfig", menuName = "PowerUp Config", order = 3)]
public class PowerUpConfig : ScriptableObject
{
    [Header("PowerUp Type")]
    public POWERUP_TYPE powerUpType;

    [Header ("PowerUp Data")]
    public float powerUpLifeSpan;
    public int boostAmount;
    public Sprite icon;
    public int consumablePowerTime;
}