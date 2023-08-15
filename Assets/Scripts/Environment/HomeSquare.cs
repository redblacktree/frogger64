using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSquare : MonoBehaviour
{
    public bool Occupied = false;
    public Vector2 HomeLocation;
    private Fly fly;

    private void Awake()
    {
        fly = GetComponentInChildren<Fly>();
    }

    public void Reset()
    {
        Occupied = false;
        fly.Enable();
    }

    public void Occupy()
    {
        Occupied = true;
        fly.Disable();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered home square at pos:" + HomeLocation);
            Debug.Log("Player entered home square at pos:" + gameObject.transform.position);
            if (Occupied)
            {
                GameManager.Instance.Player.Die();
            }
            else
            {
                GameManager.Instance.PlayerHome();
                Occupy();
            }
        }
    }
}
