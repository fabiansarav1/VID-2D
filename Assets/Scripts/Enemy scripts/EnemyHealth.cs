using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;

    private SpriteRenderer spriteRenderer;
    private EnemyPatrol patrol;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        patrol = GetComponent<EnemyPatrol>();
    }

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        if (patrol != null)
            patrol.enabled = false;

        Collider2D col = GetComponent<Collider2D>();

        if (col != null)
            col.enabled = false;

        float time = 0f;

        Color color = spriteRenderer.color;

        while (time < 1f)
        {
            time += Time.deltaTime;

            color.a = Mathf.Lerp(1f, 0f, time);
            spriteRenderer.color = color;

            yield return null;
        }

        Destroy(gameObject);
    }
}