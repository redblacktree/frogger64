using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int LevelNumber;
    public List<SpawnerData> Spawners;
}

[System.Serializable]
public class SpawnerData
{
    public List<MobData> Mobs;
    public float Speed;
}

[System.Serializable]
public class MobData
{
    public string Type;
    public float SpawnDelay;        
    public bool Dives;
}
