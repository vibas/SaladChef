using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Player playerScript;
    Vector2 velocity;
    Rigidbody2D rb2D;

    private void Awake()
    {       
        playerScript = GetComponent<Player>();
        rb2D = GetComponent<Rigidbody2D>();
    }     

    private void FixedUpdate()
    {
        if (!playerScript.CanMove())
            return;
        
        if (Input.GetKey(playerScript.inputConfig.rightMovementKey))
        {
            rb2D.MovePosition(rb2D.position + Vector2.right * Time.deltaTime * playerScript.movementSpeed);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        if (Input.GetKey(playerScript.inputConfig.leftMovementKey))
        {
            rb2D.MovePosition(rb2D.position + Vector2.left * Time.deltaTime * playerScript.movementSpeed);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }
        if (Input.GetKey(playerScript.inputConfig.UpMovementKey))
        {
            rb2D.MovePosition(rb2D.position + Vector2.up * Time.deltaTime * playerScript.movementSpeed);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        if (Input.GetKey(playerScript.inputConfig.DownMovementKey))
        {
            rb2D.MovePosition(rb2D.position + Vector2.down * Time.deltaTime * playerScript.movementSpeed);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }   
    
}
