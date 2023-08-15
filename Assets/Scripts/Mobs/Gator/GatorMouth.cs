using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatorMouth : MonoBehaviour
{
     public bool IsDeadly = true;

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (IsDeadly && other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (!player.Jumping)
            {
                player.Die();
            }
        }   
    }
}
