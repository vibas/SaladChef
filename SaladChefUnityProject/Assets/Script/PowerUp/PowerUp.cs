using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attached to powerup Game Object
/// Instantiates powerup object at random place and intialize its properties
/// </summary>
public class PowerUp : MonoBehaviour
{    
    int playerID;       // Which player can collect this powerup
    
    public POWERUP_TYPE powerUpType;    
    public float lifeSpan;      // How long power up stays before it disappears
    public int boostAmount;     // Amount to be added (Timer, Score ect)

    float timer;
    bool shouldRunTimer;

    public Image icon;
    public Image lifeSpanProgressBar;

    public void InitializePowerUpData(PowerUpConfig powerUpConfig)
    {
        powerUpType = powerUpConfig.powerUpType;
        this.name = powerUpType.ToString();
        lifeSpan = powerUpConfig.powerUpLifeSpan;
        boostAmount = powerUpConfig.boostAmount;
        icon.sprite = powerUpConfig.icon;
    }

	public void AssignPowerUpForPlayer(int playerID)
    {
        this.playerID = playerID;
    }

    private void Start()
    {
        timer = 0;
        shouldRunTimer = true;
    }

    private void Update()
    {
        if(GameManager._instance.isGamePaused || GameManager._instance.isGameOver)
        {
            return;
        }

        if(shouldRunTimer)
        {           
            timer += Time.deltaTime;
            lifeSpanProgressBar.fillAmount = timer / lifeSpan;
            if (timer >= lifeSpan)
            {
                shouldRunTimer = false;               
                DestroyPowerUp();
            }
        }        
    }

    /// <summary>
    /// When player collects the powerup it is destroyed and powerup is applied to player
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if(player.playerID.Equals(this.playerID))
            {
                player.OnPowerUpCollected(this);               
                DestroyPowerUp();
            }            
        }       
    }

    public void DestroyPowerUp()
    {
        if(GameManager._instance.powerUpManagerInstance.allPowerUps.Contains(this))
        {
            GameManager._instance.powerUpManagerInstance.allPowerUps.Remove(this);
        }
        Destroy(this.gameObject);
    }
}
