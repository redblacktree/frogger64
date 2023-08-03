using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Vector2 mapGridOffset;
    [SerializeField] private Vector2 playerSpawnPoint;
    public static GameManager instance;

    public Map map;
    public Player player { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
    }

    private void Start()
    {
        map = new Map(7, 8, mapGridOffset);
        GameObject playerObject = Instantiate(playerPrefab, map.GridCoordToWorldPoint(playerSpawnPoint), Quaternion.identity);
        player = playerObject.GetComponent<Player>();        
    }
}
