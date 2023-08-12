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
        float simulatedTime = 20f;
        while (simulatedTime > 0)
        {            
            foreach (MobData mobData in Mobs)
            {
                simulatedTime -= mobData.SpawnDelay;
                float spawnX = transform.position.x + ((mobData.SpawnDelay + simulatedTime) * Speed * MoveDirection);
                SpawnMob(mobData, new Vector2(spawnX, transform.position.y));
                if (simulatedTime < 0)
                {
                    break;
                }
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
            foreach (MobData mobData in Mobs)
            {
                yield return new WaitForSeconds(mobData.SpawnDelay);
                SpawnMob(mobData, transform.position);                
            }            
        }
    }

    private void SpawnMob(MobData mobData, Vector2 position)
    {
        GameObject mobPrefab = Resources.Load<GameObject>($"Prefabs/{mobData.Type}");
        GameObject mobObject = Instantiate(mobPrefab, transform);
        mobObject.transform.position = position;
        mobObject.GetComponent<Mob>().Speed = Speed;
        mobObject.GetComponent<Mob>().MoveDirection = MoveDirection;
        mobObject.GetComponent<Mob>().Dives = mobData.Dives;
    }
}
