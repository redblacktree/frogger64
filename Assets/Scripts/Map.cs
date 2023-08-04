using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Vector2 mapGridOffset;
    [SerializeField] private Vector2 playerSpawnPoint;
    [SerializeField] private Vector2 lowerBounds;
    [SerializeField] private Vector2 upperBounds;

    public Vector2 PlayerSpawnPoint => GridCoordToWorldPoint(playerSpawnPoint);

    public bool IsInBounds(Vector2 gridPoint) => gridPoint.x >= lowerBounds.x && gridPoint.x <= upperBounds.x && gridPoint.y >= lowerBounds.y && gridPoint.y <= upperBounds.y;

    public Vector2 GridCoordToWorldPoint(Vector2 gridPoint) => new(gridPoint.x + mapGridOffset.x, gridPoint.y + mapGridOffset.y);

    public Vector2 WorldPointToGridCoord(Vector2 worldPoint) => new(worldPoint.x - mapGridOffset.x, worldPoint.y - mapGridOffset.y);
}
