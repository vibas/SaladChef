using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerInputConfig inputConfig;
    public float movementSpeed;

    public int playerID;
    public string playerName;
    public SpriteRenderer playerSpriteRenderer;

    public void InitPlayer(PlayerConfig playerConfig)
    {
        playerID = playerConfig.playerID;
        playerName = playerConfig.playerName;
        this.name = playerName;
        playerSpriteRenderer.sprite = playerConfig.playerImage;

        inputConfig = playerConfig.inputConfig;
        movementSpeed = playerConfig.initialMovementSpeed;        
    }	
}
