using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Entity
{
    public float Speed = 1f;
    public int MoveDirection = 1;
    public bool Dives = false;

    protected override void Start()
    {
        base.Start();

        Debug.Log($"Mob Start {gameObject.name} MoveDirection {MoveDirection}");
        FlipSprite(new Vector2(MoveDirection, 0));
    }

    protected override void Update()
    {    
        base.Update();
        transform.Translate(new Vector2(MoveDirection * Speed * Time.deltaTime, 0f));

        if (transform.position.x > 20f || transform.position.x < -20f)
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
