using UnityEngine;

/// <summary>
/// Attched to player Game Object to detect collision
/// </summary>
public class PlayerCollisionChecker : MonoBehaviour
{
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
