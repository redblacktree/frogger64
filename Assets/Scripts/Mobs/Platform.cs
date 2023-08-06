using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : Mob
{
    protected override void OnTriggerEnter2D(Collider2D other) 
    {
        base.OnTriggerEnter2D(other);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit platform: " + gameObject.name);
            Player player = other.GetComponent<Player>();
            player.Riding = true;
            player.transform.SetParent(transform);
        }   
    }

    protected override void OnTriggerExit2D(Collider2D other) {
        base.OnTriggerExit2D(other);
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.Riding = false;
            player.transform.SetParent(null);
        }
    }
}
