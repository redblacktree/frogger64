using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private int lives = 6;
    [SerializeField] private float respawnTime = 2f;
    [SerializeField] private float timeLimit = 30f;
    public float MovementDeathDistance = 0.5f;

    public static GameManager Instance;

    public Map map;
    public Player Player { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
        map = GetComponentInChildren<Map>();
    }

    private void Start()
    {
        GameObject playerObject = Instantiate(playerPrefab, map.PlayerSpawnPoint, Quaternion.identity);
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
            GameObject playerObject = Instantiate(playerPrefab, map.PlayerSpawnPoint, Quaternion.identity);
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
        StartCoroutine(RespawnPlayerCoroutine());
    }
}
