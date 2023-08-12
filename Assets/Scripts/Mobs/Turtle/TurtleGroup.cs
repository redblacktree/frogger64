using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleGroup : Mob
{
    [SerializeField] private GameObject turtlePrefab;
    [SerializeField] private int turtleCount = 3;
    [SerializeField] private float turtleSpacing = 1.0f;

    protected override void Start()
    {
        base.Start();
        
        for (int i = 0; i < turtleCount; i++)
        {
            GameObject gameObject = Instantiate(turtlePrefab, transform);
            Turtle turtle = gameObject.GetComponent<Turtle>();
            turtle.transform.localPosition = new Vector3(i * turtleSpacing, 0, 0);
            turtle.Speed = 0;
            turtle.MoveDirection = this.MoveDirection;
            turtle.Dives = this.Dives;
        }
    }
}
