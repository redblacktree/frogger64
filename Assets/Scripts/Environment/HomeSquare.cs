using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSquare : MonoBehaviour
{
    public bool Occupied { get; private set; } = false;
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
            GameManager.Instance.PlayerHome();
        }
    }
}
