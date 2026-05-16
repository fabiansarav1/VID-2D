using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;                 // Velocidad de movimiento
    public Transform groundCheck;            // Punto debajo del enemigo para detectar suelo
    public float groundCheckDistance = 1f;   // Distancia para detectar el fin del suelo
    public LayerMask groundLayer;            // Capa del suelo

    public Animator animator;                // Referencia al Animator
    private bool movingRight = true;         // Dirección actual

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("Isrunning", true);
    }

    private void Update()
    {
        // Movimiento horizontal
        rb.linearVelocity = new Vector2((movingRight ? 1 : -1) * speed, rb.linearVelocity.y);

        // Detectar si se termina la plataforma
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        if (!groundInfo.collider)
        {
            Flip();
        }
        RaycastHit2D wallInfo = Physics2D.Raycast(groundCheck.position, movingRight ? Vector2.right : Vector2.left, 0.2f, groundLayer);
if (wallInfo.collider)
{
    Flip();
}

    }

    private void Flip()
    {
        movingRight = !movingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // voltear sprite
        transform.localScale = localScale;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        collision.gameObject.GetComponent<PlayerHealth>().TakeDamage();
    }
}

}
