using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
    public void UpdateLives(int lives)
    {        
        for (int i = 0; i < transform.childCount; i++)
        {
            var lifeIcon = transform.GetChild(i);
            if (i < lives)
            {
                lifeIcon.gameObject.SetActive(true);
            }
            else
            {
                lifeIcon.gameObject.SetActive(false);
            }
        }
    }
}
