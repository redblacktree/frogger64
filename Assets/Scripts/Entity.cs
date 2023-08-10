using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public LayerMask enemyLayer;

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Collider2D col { get; private set; }

    public Vector2 facingDirection { get; private set; } = Vector2.up;
    
    protected virtual void Awake() {}

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponentInChildren<Animator>();
        if (anim == null) 
        {
            anim = GetComponent<Animator>();
        }
    }

    protected virtual void Update() {}

    public virtual void Flip(Vector2 direction)
    {
        if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 270f);
        }
        else if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (direction.y > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (direction.y < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
    }

    public virtual void FlipSprite(Vector2 direction)
    {
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
