using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public float playerSpeed;

    private AudioSource playerAudio;
    private Rigidbody2D rbPlayer;
    private InputAction playerMove, playerJump;
    private Vector2 moveInput;

    Animator playerAnimator;

    // 🔽 Control de saltos
    private int jumpCount = 0;
    public int maxJumps = 2;

    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        playerMove = InputSystem.actions.FindAction("Move");
        playerJump = InputSystem.actions.FindAction("Jump");
    }

    private void OnEnable()
    {
        playerJump.started += _ => Jump();
    }

    private void OnDisable()
    {
        playerJump.started -= _ => Jump();
    }

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    void Move()
    {
        rbPlayer.linearVelocityX = moveInput.x * playerSpeed;

        if (rbPlayer.linearVelocityX != 0)
        {
            playerAnimator.SetBool("Isrunning", true);

            if (rbPlayer.linearVelocityX < 0)
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
        }
        else
        {
            playerAnimator.SetBool("Isrunning", false);
        }
    }

    void MoveInputDetected()
    {
        moveInput = playerMove.ReadValue<Vector2>();
    }

    void Update()
    {
        MoveInputDetected();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Jump()
    {
        if (jumpCount < maxJumps)
        {
            // Resetear velocidad vertical para un salto más consistente
            rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, 0f);

            rbPlayer.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    // 🔽 Detectar suelo y reiniciar saltos
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }
}