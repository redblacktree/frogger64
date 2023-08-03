using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public LayerMask enemyLayer;

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public int facingDirection { get; private set; } = 1;
    private bool isFacingRight = true;

    protected virtual void Awake() {}

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        if (anim == null) 
        {
            anim = GetComponent<Animator>();
        }
        Debug.Log(anim);
    }

    protected virtual void Update() {}

    public virtual void Flip()
    {
        facingDirection *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public virtual void FlipController(float x)
    {
        if (x > 0 && !isFacingRight || x < 0 && isFacingRight)
        {
            Flip();
        }
    }
    
    #region Velocity
    public void SetVelocity(float xVelocity, float yVelocity, bool flip = true)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        if (flip)
        {
            FlipController(xVelocity);
        }
    }

    public void SetVelocity(Vector2 velocity, bool flip = true)
    {
        SetVelocity(velocity.x, velocity.y, flip);
    }

    public void SetZeroVelocity() 
    {
        SetVelocity(0f, 0f, false);
    }
    #endregion    
}
