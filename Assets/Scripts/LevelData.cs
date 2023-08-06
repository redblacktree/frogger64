using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int LevelNumber;
    public List<MobData> Mobs;
}

[System.Serializable]
public class MobData
{
    public string Type;
    public float SpawnFrequency;
    public Vector2 SpawnPoint;
    public float Speed;
    public int MoveDirection;
}
