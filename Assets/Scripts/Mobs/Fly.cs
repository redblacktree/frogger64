using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Entity
{
    [SerializeField] private float appearanceFrequency = 30f;
    [SerializeField] private float appearanceDuration = 2f;
    [SerializeField] private float variance = 15f;    

    protected override void Start()
    {
        base.Start();
        Disappear();
        StartCoroutine(AppearCoroutine());
    }

    private void Appear()
    {
        sr.enabled = true;
        col.enabled = true;
    }

    private void Disappear()
    {
        sr.enabled = false;
        col.enabled = false;
    }

    public void Enable()
    {
        StartCoroutine(AppearCoroutine());
    }

    public void Disable()
    {
        StopAllCoroutines();
        Disappear();
    }

    private IEnumerator DisappearCoroutine()
    {
        yield return new WaitForSeconds(appearanceDuration);
        Disappear();
        StartCoroutine(AppearCoroutine());
    }

    private IEnumerator AppearCoroutine()
    {
        yield return new WaitForSeconds(appearanceFrequency + Random.Range(-variance, variance));
        Appear();
        StartCoroutine(DisappearCoroutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(GameManager.Instance.ScoreForEatingFly);
            Disappear();
        }
    }
}
