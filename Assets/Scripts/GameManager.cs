using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

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
        map = GetComponentInChildren<Map>();
    }

    private void Start()
    {
        GameObject playerObject = Instantiate(playerPrefab, map.PlayerSpawnPoint, Quaternion.identity);
        player = playerObject.GetComponent<Player>();
    }
}
