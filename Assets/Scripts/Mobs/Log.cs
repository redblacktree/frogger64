using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Platform
{
    private float girlfriendSpawnRate = 1f;

    protected override void Start()
    {
        base.Start();

        if (Random.Range(0f, 1f) < girlfriendSpawnRate)
        {
            SpawnGirlfriend();
        }
    }

    private void SpawnGirlfriend()
    {
        GameObject gameObject = Instantiate(Resources.Load<GameObject>("Prefabs/Girlfriend"));
        Girlfriend girlfriend = gameObject.GetComponent<Girlfriend>();
        girlfriend.transform.position = transform.position + new Vector3(0, 0, 0);
        girlfriend.FaceDirection(Vector2.right);
        girlfriend.transform.parent = transform;
    }
}
