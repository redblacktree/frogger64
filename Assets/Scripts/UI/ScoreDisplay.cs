using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private float lineSpacing = 0.5f;

    public int Score { get; private set; } = 12345;
    private string ScoreString { get { return Score.ToString("D5"); } }
    private Dictionary<char, GameObject> scoreNumberPrefabs = new Dictionary<char, GameObject>();    

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            scoreNumberPrefabs.Add(i.ToString()[0], Resources.Load<GameObject>("Prefabs/Score" + i));
        }
    }

    public void UpdateScore(int score)
    {
        Score = score;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        AddScoreNumbers();
    }

    private void AddScoreNumbers()
    {
        int digitPosition = 0;
        foreach (char digit in ScoreString)
        {
            GameObject scoreNumber = Instantiate(scoreNumberPrefabs[digit], transform);
            scoreNumber.transform.localPosition = new Vector3(scoreNumber.transform.localPosition.x, scoreNumber.transform.localPosition.y - (lineSpacing * digitPosition++), scoreNumber.transform.localPosition.z);
        }
    }
}
