using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    private Vector2 offset;
    private Vector2 lowerBounds;
    private Vector2 upperBounds;

    public Map(Vector2 lowerBounds, Vector2 upperBounds, Vector2 offset)
    {
        this.lowerBounds = lowerBounds;
        this.upperBounds = upperBounds;
        this.offset = offset;
    }

    public bool IsInBounds(Vector2 gridPoint) 
    {
        Debug.Log("Bounds check: " + gridPoint);
        return gridPoint.x >= lowerBounds.x && gridPoint.x <= upperBounds.x && gridPoint.y >= lowerBounds.y && gridPoint.y <= upperBounds.y;
    }

    public Vector2 GridCoordToWorldPoint(Vector2 gridPoint)
    {
        Vector2 worldPoint = new Vector2(gridPoint.x + offset.x, gridPoint.y + offset.y);
        Debug.Log("Grid: " + gridPoint + "World: " + worldPoint);
        return worldPoint;
    }

    public Vector2 WorldPointToGridCoord(Vector2 worldPoint)
    {
        return new Vector2(worldPoint.x - offset.x, worldPoint.y - offset.y);
    }
}
