using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit hazard: " + gameObject.name);
            Player player = other.GetComponent<Player>();
            if (!player.Riding)
            {
                player.Die();
            }
        }   
    }
}
