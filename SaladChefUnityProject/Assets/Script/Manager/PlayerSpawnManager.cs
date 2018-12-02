using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [Tooltip("Present inside Kitchen")]
    public Transform[] playerSpawnPointArray;

    public GameObject playerPrefab;

    public PlayerConfig player1Config, player2Config;
    
	// Use this for initialization
	public void Init ()
    {
        GameObject player1 = Instantiate(playerPrefab, playerSpawnPointArray[0].position, Quaternion.identity);
        player1.transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, 0);
        player1.GetComponent<Player>().InitPlayer(player1Config);
        player1.transform.SetParent(transform);
        GameManager._instance.AddPlayerToAllPlayerList(player1.GetComponent<Player>());

        GameObject player2 = Instantiate(playerPrefab, playerSpawnPointArray[1].position, Quaternion.identity);
        player2.transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 0);
        player2.GetComponent<Player>().InitPlayer(player2Config);
        player2.transform.SetParent(transform);
        GameManager._instance.AddPlayerToAllPlayerList(player2.GetComponent<Player>());
    }
}
