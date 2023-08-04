using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    [Header("Map")]
    [SerializeField] private Vector2 mapGridOffset;
    [SerializeField] private Vector2 playerSpawnPoint;
    [SerializeField] private Vector2 lowerBounds;
    [SerializeField] private Vector2 upperBounds;

    public static GameManager Instance;

    public Map map;
    public Player player { get; private set; }

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
        map = new Map(lowerBounds, upperBounds, mapGridOffset);
        GameObject playerObject = Instantiate(playerPrefab, map.GridCoordToWorldPoint(playerSpawnPoint), Quaternion.identity);
        player = playerObject.GetComponent<Player>();        
    }
}
