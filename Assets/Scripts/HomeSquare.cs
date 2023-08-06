using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSquare : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.PlayerHome();
        }
    }
}
