using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public float Speed = 1f;
    public int MoveDirection = 1;
    public float SpawnFrequency = 1f;
    public Vector2 size = Vector2.one;

    void Update()
    {    
        transform.Translate(new Vector2(MoveDirection * Speed * Time.deltaTime, 0f));

        if (transform.position.x > 10f || transform.position.x < -10f)
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) 
    {
    }

    protected virtual void OnTriggerExit2D(Collider2D other) 
    {
    }

    protected virtual void OnTriggerStay2D(Collider2D other) 
    {
    }
}
