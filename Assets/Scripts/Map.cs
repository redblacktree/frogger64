using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class Map : MonoBehaviour
{
    [SerializeField] private Vector2 playerSpawnPoint;
    [SerializeField] private Vector2 mapGridOffset;
    [SerializeField] private Vector2 lowerBounds;
    [SerializeField] private Vector2 upperBounds;
    [SerializeField] private Vector2[] deadlyObstacles;
    [SerializeField] private Vector2[] winSquares;
    public Vector2[] OccupiedWinSquares;

    [SerializeField] private bool drawGizmos = true;

    private void Awake() {
        OccupiedWinSquares = new Vector2[winSquares.Length];
    }

    public Vector2 PlayerSpawnPoint => GridCoordToWorldPoint(playerSpawnPoint);

    public bool IsInBounds(Vector2 gridPoint) => gridPoint.x >= lowerBounds.x && gridPoint.x <= upperBounds.x && gridPoint.y >= lowerBounds.y && gridPoint.y <= upperBounds.y;

    public bool IsDeadly(Vector2 gridPoint) => IsInBounds(gridPoint) 
        && (System.Array.Exists(deadlyObstacles, element => element == gridPoint) 
            || IsOccupiedWinSquare(gridPoint));

    public bool IsWinSquare(Vector2 gridPoint) => IsInBounds(gridPoint) 
        && System.Array.Exists(winSquares, element => element == gridPoint) 
        && !System.Array.Exists(OccupiedWinSquares, element => element == gridPoint);

    public bool IsOccupiedWinSquare(Vector2 gridPoint) => IsInBounds(gridPoint) 
        && System.Array.Exists(winSquares, element => element == gridPoint)
        && System.Array.Exists(OccupiedWinSquares, element => element == gridPoint);

    public Vector2 GridCoordToWorldPoint(Vector2 gridPoint) => new(gridPoint.x + mapGridOffset.x, gridPoint.y + mapGridOffset.y);

    public Vector2 WorldPointToGridCoord(Vector2 worldPoint) => new(worldPoint.x - mapGridOffset.x, worldPoint.y - mapGridOffset.y);

    public void Reset()
    {
        OccupiedWinSquares = new Vector2[winSquares.Length];
    }

    public void OccupyWinSquare(Vector2 gridPoint)
    {
        if (IsWinSquare(gridPoint))
        {
            int index = System.Array.IndexOf(winSquares, gridPoint);
            OccupiedWinSquares[index] = gridPoint;
        }
    }

    private void OnDrawGizmos() {
        if (!drawGizmos) return;
        for (int x = (int)lowerBounds.x; x <= (int)upperBounds.x; x++)
        {
            for (int y = (int)lowerBounds.y; y <= (int)upperBounds.y; y++)
            {
                Vector2 gridPoint = new(x, y);
                Vector2 worldPoint = GridCoordToWorldPoint(gridPoint);
                Gizmos.color = IsDeadly(gridPoint) ? Color.red : Color.white;
                Gizmos.DrawWireCube(worldPoint, Vector2.one);
                
                #if UNITY_EDITOR
                Vector2 labelPosition = worldPoint - new Vector2(0.4f, 0);
                Handles.Label(labelPosition, $"{x}, {y}");                
                #endif
            }
        }        
    }
}
