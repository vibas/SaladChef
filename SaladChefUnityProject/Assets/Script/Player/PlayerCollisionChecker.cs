using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionChecker : MonoBehaviour {

    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractibleKitchenElement"))
        {
            collision.GetComponent<InteractibleKitchenElement>().PlayerReached(player);
        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractibleKitchenElement"))
        {
            collision.GetComponent<InteractibleKitchenElement>().PlayerLeft(player);
        }
    }
}
