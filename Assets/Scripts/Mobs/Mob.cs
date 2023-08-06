using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Vector2 direction = Vector2.right;
    [SerializeField] private Vector2 size = Vector2.one;

    void Update()
    {
        //transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
