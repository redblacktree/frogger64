using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private LivesDisplay livesDisplay;
    [SerializeField] private ScoreDisplay scoreDisplay;
    [SerializeField] private ScoreDisplay highScoreDisplay;
    [SerializeField] private MessageDisplay messageDisplay;
    public List<HomeSquare> HomeSquares = new List<HomeSquare>();
    public List<Spawner> Spawners = new List<Spawner>();

    [Header("Transition Settings")]
    [SerializeField] private float newLevelLoadDelay = 2f;
    [SerializeField] private float gameOverDelay = 5f;

    [Header("Scoring Events")]
    public int ScorePerHomeSquare = 50;
    public int ScorePerJumpForward = 10;
    public int ScoreForSavingGirlfriend = 200;
    public int ScoreForEatingFly = 200;
    public int ScorePerSecondRemaining = 10;

    [Header("Girlfriend")]
    public float GirlfriendAppearanceFrequency = 0.2f;

    [Header("Player Data")]
    [SerializeField] private Vector2 playerSpawnPoint = new Vector2(3.5f, -4f);
    [SerializeField] private int lives = 6;
    [SerializeField] private float respawnTime = 2f;
    public float TimeLimit = 30f;

    public static GameManager Instance;

    public Player Player { get; private set; }
    public Girlfriend Girlfriend { get; set; }
    private LevelData levelData;    
    public float TimeRemaining { get; private set; } = 0f;
    private bool TimerStopped = false;
    private int livesRemaining  = 0;
    private int score = 0;
    private int highScore = 4630;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
        livesRemaining = lives;
    }

    private void Start()
    {
        LoadLevel(1);
        scoreDisplay.UpdateScore(0);
        highScoreDisplay.UpdateScore(highScore);
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
        this.score += score;
        if (this.score > highScore)
        {
            highScore = this.score;
        }
        scoreDisplay.UpdateScore(this.score);
        highScoreDisplay.UpdateScore(highScore);
    }
    #endregion

    #region Player
    private void SpawnPlayer()
    {
        GameObject playerObject = Instantiate(playerPrefab, playerSpawnPoint, Quaternion.identity);
        Player = playerObject.GetComponent<Player>();
        TimeRemaining = TimeLimit;
        TimerStopped = false;       
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

    public void PlayerDied()
    {
        livesRemaining--;
        livesDisplay.UpdateLives(livesRemaining);
        if (livesRemaining > 0)
        {
            SpawnPlayer();
        }
        else
        {
            StartCoroutine(GameOverCoroutine());
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

    private IEnumerator GameOverCoroutine()
    {
        DisplayMessage("game over", gameOverDelay);
        TimerStopped = true;
        yield return new WaitForSeconds(gameOverDelay);
        this.score = 0;
        scoreDisplay.UpdateScore(this.score);
        LoadLevel(1);
    }
    #endregion
}
