using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDisplay : MonoBehaviour
{
    [SerializeField] private GameObject MessageBackground;
    [SerializeField] private float letterSpacing = 0.5f;    

    private List<char> availableLetters = new List<char>() {'A', 'C', 'E', 'G', 'H', 'I', 'M', 'O', 'P', 'R', 'S', 'T', 'U', 'V'};
    private List<char> availableNumbers = new List<char>() {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
    private Dictionary<char, GameObject> characterPrefabs = new Dictionary<char, GameObject>();
    private const float onePixel = 0.125f;

    private void Awake()
    {
        foreach (char c in availableLetters)
        {
            characterPrefabs.Add(c, Resources.Load<GameObject>("Prefabs/letter" + c));
        }
        foreach (char c in availableNumbers)
        {
            characterPrefabs.Add(c, Resources.Load<GameObject>("Prefabs/number" + c));
        }
    }

    public void DisplayMessage(string text, float duration)
    {
        StartCoroutine(DisplayMessageCoroutine(text, duration));
    }

    private IEnumerator DisplayMessageCoroutine(string text, float duration)
    {
        MessageBackground.SetActive(true);
        MessageBackground.transform.localScale = new Vector3(text.Length * letterSpacing + (2 * onePixel), MessageBackground.transform.localScale.y, MessageBackground.transform.localScale.z);
        DisplayMessageLetters(text);
        yield return new WaitForSeconds(duration);        
        foreach (Transform child in transform)
        {
            if (child.gameObject != MessageBackground)
            {
                Destroy(child.gameObject);
            }
        }
        MessageBackground.SetActive(false);
    }

    private void DisplayMessageLetters(string text)
    {
        text = text.ToUpper();
        int letterPosition = 0;
        foreach (char letter in text)
        {
            if (!characterPrefabs.ContainsKey(letter))
            {
                letterPosition++;
                continue;
            }
            GameObject letterPrefab = characterPrefabs[letter];
            GameObject letterObject = Instantiate(letterPrefab, transform);
            letterObject.transform.localPosition = new Vector3(letterObject.transform.localPosition.x + (letterSpacing * letterPosition++) - (text.Length * letterSpacing / 2), letterObject.transform.localPosition.y, letterObject.transform.localPosition.z);
        }        
    }
}
