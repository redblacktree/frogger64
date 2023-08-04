using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public LayerMask enemyLayer;

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public Vector2 facingDirection { get; private set; } = Vector2.up;
    
    protected virtual void Awake() {}

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        if (anim == null) 
        {
            anim = GetComponent<Animator>();
        }
    }

    protected virtual void Update() {}
}
