using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<MobData> Mobs;
    public int MoveDirection;
    public float Speed;
    public int SpawnerIndex;

    public void ResetSpawner()
    {
        StopAllCoroutines();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        float nextMobXPos = 0;
        while (Mathf.Abs(nextMobXPos) < 20f)
        {
            // we want to work backward through the list because we're "going back in time" to spawn the mobs
            for (int i = Mobs.Count - 1; i >= 0; i--)
            {
                MobData mobData = Mobs[i];
                Mob mob = SpawnMob(mobData, transform.position);
                nextMobXPos += mobData.Spacing * MoveDirection;
                Vector2 spawnPos = new Vector2(nextMobXPos, 0);
                mob.transform.localPosition = spawnPos;
            }
        }

        StartCoroutine(SpawnMobsCoroutine());
    }

    public void LoadLevelData(SpawnerData spawnerData)
    {
        Mobs = spawnerData.Mobs;
        Speed = spawnerData.Speed;
    }

    private IEnumerator SpawnMobsCoroutine()
    {
        while (true)
        {
            foreach(MobData mobData in Mobs)
            {
                SpawnMob(mobData, transform.position);
                yield return new WaitForSeconds(mobData.Spacing / Speed);            
            }
        }
    }

    private Mob SpawnMob(MobData mobData, Vector2 position)
    {
        GameObject mobPrefab = Resources.Load<GameObject>($"Prefabs/{mobData.Type}");
        GameObject mobObject = Instantiate(mobPrefab, transform);
        
        Mob mob = mobObject.GetComponent<Mob>();
        mob.transform.position = position;
        mob.Speed = Speed;
        mob.MoveDirection = MoveDirection;
        mob.Dives = mobData.Dives;

        return mob;
    }
}
