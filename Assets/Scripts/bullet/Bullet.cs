using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    private int direction;

    public void SetDirection(int dir)
    {
        direction = dir;
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage();
            Destroy(gameObject);
            return;
        }

        if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}