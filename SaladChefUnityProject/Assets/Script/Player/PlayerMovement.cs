using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Player playerScript;
    Vector2 velocity;
    Transform playerTransform;

    private void Awake()
    {
        playerTransform = transform;
        playerScript = GetComponent<Player>();
    }     

    private void FixedUpdate()
    {
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        if (Input.GetKey(playerScript.inputConfig.rightMovementKey))
        {
            rb2D.MovePosition(rb2D.position + Vector2.right * Time.deltaTime * playerScript.movementSpeed);
        }
        if (Input.GetKey(playerScript.inputConfig.leftMovementKey))
        {
            rb2D.MovePosition(rb2D.position + Vector2.left * Time.deltaTime * playerScript.movementSpeed);
        }
        if (Input.GetKey(playerScript.inputConfig.UpMovementKey))
        {
            rb2D.MovePosition(rb2D.position + Vector2.up * Time.deltaTime * playerScript.movementSpeed);
        }
        if (Input.GetKey(playerScript.inputConfig.DownMovementKey))
        {
            rb2D.MovePosition(rb2D.position + Vector2.down * Time.deltaTime * playerScript.movementSpeed);
        }
    }   
}
