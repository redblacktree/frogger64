using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Mob
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTriggerStay2D(Collider2D other) 
    {
        base.OnTriggerStay2D(other);
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.Die();
        }
    }
}
