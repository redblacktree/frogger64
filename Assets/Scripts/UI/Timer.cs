using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private Vector2 initialScale;

    private void Start()
    {
        initialScale = transform.localScale;
    }

    private void Update()
    {
        float value = Mathf.Clamp(GameManager.Instance.TimeRemaining / GameManager.Instance.TimeLimit, 0, 1);
        transform.localScale = new Vector3(value * initialScale.x, initialScale.y, 1);
    }
}
