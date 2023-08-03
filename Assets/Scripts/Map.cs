using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int[,] Grid { get; private set; }

    private Vector2 offset;

    public Map(int width, int height, Vector2 offset)
    {
        Width = width;
        Height = height;
        Grid = new int[width, height];        
    }

    public Vector2 GridCoordToWorldPoint(Vector2 gridPoint)
    {
        return new Vector2(gridPoint.x + offset.x, gridPoint.y + offset.y);
    }

    public Vector2 WorldPointToGridCoord(Vector2 worldPoint)
    {
        return new Vector2(worldPoint.x - offset.x, worldPoint.y - offset.y);
    }
}
