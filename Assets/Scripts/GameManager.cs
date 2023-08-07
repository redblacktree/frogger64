using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject mobsParent;
    [SerializeField] private Vector2 playerSpawnPoint = new Vector2(3.5f, -4f);
    [SerializeField] private int lives = 6;
    [SerializeField] private float respawnTime = 2f;
    public float TimeLimit = 30f;

    public static GameManager Instance;

    public Player Player { get; private set; }

    private LevelData levelData;
    private int homeSquares = 3;
    private int froggersHome = 0;
    public float TimeRemaining = 0f;

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
        SpawnPlayer();
        LoadLevel(1);
        SpawnMobs();
    }

    private void Update()
    {
        TimeRemaining -= Time.deltaTime;
        if (TimeRemaining <= 0)
        {
            Player.Die();
        }
    }

    #region Player
    private void SpawnPlayer()
    {
        if (lives > 0)
        {
            GameObject playerObject = Instantiate(playerPrefab, playerSpawnPoint, Quaternion.identity);
            Player = playerObject.GetComponent<Player>();
            TimeRemaining = TimeLimit;
            if (!Player.HomeSafe)
            {
                lives--;
            }
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
    #endregion

    #region Level
    private void SpawnMobs()
    {
        foreach (MobData mobData in levelData.Mobs)
        {
            StartCoroutine(SpawnCoroutine(mobData));            
        }
    }

    private IEnumerator SpawnCoroutine(MobData mobData)
    {
        while(true)
        {
            SpawnMob(mobData);
            if (mobData.SpawnFrequency > 0)
            {
                yield return new WaitForSeconds(mobData.SpawnFrequency);                
            }
            else
            {
                break;
            }            
        }        
    }

    private void SpawnMob(MobData mobData)
    {
        GameObject mobPrefab = Resources.Load<GameObject>($"Prefabs/{mobData.Type}");
        GameObject mobObject = Instantiate(mobPrefab, mobsParent.transform);
        mobObject.transform.position = mobData.SpawnPoint;
        mobObject.GetComponent<Mob>().Speed = mobData.Speed;
        mobObject.GetComponent<Mob>().MoveDirection = mobData.MoveDirection;
    }
    #endregion

    public void LoadLevel(int level)
    {
        TextAsset jsonTextAsset = Resources.Load<TextAsset>($"Levels/Level{level}");
        levelData = JsonUtility.FromJson<LevelData>(jsonTextAsset.text);
    }
}
