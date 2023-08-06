using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Vector2 playerSpawnPoint = new Vector2(3.5f, -4f);
    [SerializeField] private int lives = 6;
    [SerializeField] private float respawnTime = 2f;
    [SerializeField] private float timeLimit = 30f;
    public float MovementDeathDistance = 0.5f;

    public static GameManager Instance;

    public Player Player { get; private set; }

    private int homeSquares = 3;
    private int froggersHome = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        GameObject playerObject = Instantiate(playerPrefab, playerSpawnPoint, Quaternion.identity);
        Player = playerObject.GetComponent<Player>();
    }

    private void SpawnPlayer()
    {
        if (lives > 0)
        {
            if (!Player.HomeSafe)
            {
                lives--;
            }
            GameObject playerObject = Instantiate(playerPrefab, playerSpawnPoint, Quaternion.identity);
            Player = playerObject.GetComponent<Player>();            
        }
        else
        {
            Debug.Log("Game Over");
        }        
    }

    private IEnumerator RespawnPlayerCoroutine()
    {
        var yieldFor = respawnTime;
        if (Player.HomeSafe)
        {
            yieldFor = 0;
        }
        yield return new WaitForSeconds(yieldFor);
        SpawnPlayer();
    }

    public void RespawnPlayer()
    {
        Destroy(Player.gameObject);
        StartCoroutine(RespawnPlayerCoroutine());
    }

    public void PlayerHome()
    {
        froggersHome++;
        Player.Home();
        if (froggersHome >= homeSquares)
        {
            Debug.Log("You win!");
        }
        StartCoroutine(RespawnPlayerCoroutine());
    }
}
