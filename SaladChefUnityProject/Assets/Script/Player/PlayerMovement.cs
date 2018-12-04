using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    Player playerScript;    
    Rigidbody2D rb2D;
    int speedFactor;

    private void Awake()
    {       
        playerScript = GetComponent<Player>();
        rb2D = GetComponent<Rigidbody2D>();
        speedFactor = 1;
    }     

    public void BoostSpeed(int boostAmount, float waitTimer)
    {
        speedFactor = boostAmount;
        StartCoroutine(BackToNormalSpeed(waitTimer));
    }    

    IEnumerator BackToNormalSpeed(float waitTimer)
    {
        yield return new WaitForSeconds(waitTimer);
        speedFactor = 1;
    }

    private void FixedUpdate()
    {
        if (!playerScript.CanMove())
            return;
        
        if (Input.GetKey(playerScript.inputConfig.rightMovementKey))
        {
            rb2D.MovePosition(rb2D.position + Vector2.right * Time.deltaTime * playerScript.movementSpeed * speedFactor);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        if (Input.GetKey(playerScript.inputConfig.leftMovementKey))
        {
            rb2D.MovePosition(rb2D.position + Vector2.left * Time.deltaTime * playerScript.movementSpeed * speedFactor);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }
        if (Input.GetKey(playerScript.inputConfig.UpMovementKey))
        {
            rb2D.MovePosition(rb2D.position + Vector2.up * Time.deltaTime * playerScript.movementSpeed * speedFactor);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        if (Input.GetKey(playerScript.inputConfig.DownMovementKey))
        {
            rb2D.MovePosition(rb2D.position + Vector2.down * Time.deltaTime * playerScript.movementSpeed * speedFactor);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }   
    
}
