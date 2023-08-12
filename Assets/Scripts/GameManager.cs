using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private LivesDisplay livesDisplay;
    [SerializeField] private ScoreDisplay scoreDisplay;
    [SerializeField] private ScoreDisplay highScoreDisplay;
    [SerializeField] private MessageDisplay messageDisplay;

    [Header("Scoring Events")]
    public int ScorePerHomeSquare = 50;
    public int ScorePerJumpForward = 10;
    public int ScoreForSavingGirlfriend = 200;
    public int ScoreForEatingFly = 200;
    public int ScorePerSecondRemaining = 10;

    [SerializeField] private Vector2 playerSpawnPoint = new Vector2(3.5f, -4f);
    public List<HomeSquare> HomeSquares = new List<HomeSquare>();
    public List<Spawner> Spawners = new List<Spawner>();
    public int Lives = 6;
    [SerializeField] private float respawnTime = 2f;
    [SerializeField] private float newLevelLoadDelay = 2f;
    public float TimeLimit = 30f;

    public static GameManager Instance;

    public Player Player { get; private set; }
    private LevelData levelData;    
    public float TimeRemaining = 0f;
    private bool TimerStopped = false;

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
        LoadLevel(1);
        scoreDisplay.UpdateScore(0);
        highScoreDisplay.UpdateScore(0);
        DisplayMessage("start", 1f);
    }

    private void Update()
    {
        if (TimerStopped) return;

        TimeRemaining -= Time.deltaTime;
        if (TimeRemaining <= 0)
        {
            Player.Die();
        }
    }

    public void DisplayMessage(string message, float duration)
    {
        messageDisplay.DisplayMessage(message, duration);
    }

    #region Score
    public void AddScore(int score)
    {
        scoreDisplay.UpdateScore(scoreDisplay.Score + score);
        if (scoreDisplay.Score > highScoreDisplay.Score)
        {
            highScoreDisplay.UpdateScore(scoreDisplay.Score);
        }
    }
    #endregion

    #region Player
    private void SpawnPlayer()
    {
        if (Lives > 0)
        {
            GameObject playerObject = Instantiate(playerPrefab, playerSpawnPoint, Quaternion.identity);
            Player = playerObject.GetComponent<Player>();
            TimeRemaining = TimeLimit;
            TimerStopped = false;       
            livesDisplay.UpdateLives(Lives);
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
        StartCoroutine(RespawnPlayerCoroutine());
    }

    public void PlayerHome()
    {
        Player.Home();
        TimerStopped = true;
        if (FroggersHome >= HomeSquares.Count)
        {
            StartCoroutine(LoadNextLevelCoroutine());
        }
        else{
            StartCoroutine(RespawnPlayerCoroutine());
        }
    }
    #endregion

    #region Level
    public int FroggersHome => HomeSquares.FindAll(h => h.Occupied).Count;

    public void ResetLevel()
    {
        TimeRemaining = TimeLimit;
        HomeSquares.ForEach(h => h.Reset());
        Spawners.ForEach(s => s.ResetSpawner());
        StopAllCoroutines();
        // destroy all player objects
        foreach (Player player in FindObjectsOfType<Player>())
        {
            player.Destroy();
        }
    }

    public IEnumerator LoadNextLevelCoroutine()
    {
        yield return new WaitForSeconds(newLevelLoadDelay);
        LoadLevel(1); // TODO: Replace hard-coded level number
    }

    public void LoadLevel(int level)
    {
        TextAsset jsonTextAsset = Resources.Load<TextAsset>($"Levels/Level{level}");
        levelData = JsonUtility.FromJson<LevelData>(jsonTextAsset.text);
        Spawners.ForEach(s => s.LoadLevelData(levelData.Spawners[s.SpawnerIndex]));
        ResetLevel();
        SpawnPlayer();
    }
    #endregion
}
