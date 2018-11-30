using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Player Config", order = 1)]
public class PlayerConfig : ScriptableObject
{
    [Header("Input Keys")]
    public PlayerInputConfig inputConfig;

    [Header("Movement")]
    public float initialMovementSpeed;

    [Header("Details")]
    public int playerID;
    public string playerName;
    public Sprite playerImage;
}
