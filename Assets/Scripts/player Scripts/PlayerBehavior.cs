using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    //Estructura para crear una variable: acceso - tipo de dato - nombre de la variable
    public float playerSpeed;

    //Componente para reproducir audios
    private AudioSource playerAudio;

    private Rigidbody2D rbPlayer; //variable para el componente rigidbody del jugador
    private InputAction playerMove, playerJump; //variable para guardar el sistema de controles
    private Vector2 moveInput; //variable para guardar los valores de movimiento

    public LayerMask groundLayer;   // Para detectar el suelo
    private Transform groundDetector;

    //Componente animator
    Animator playerAnimator;

    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody2D>(); //asigno el componente rigidbody a la variable
        playerAudio = GetComponent<AudioSource>();  //asigno el componente audio source a la variable
        playerMove = InputSystem.actions.FindAction("Move"); //asigno la accion a la variable
        playerJump = InputSystem.actions.FindAction("Jump"); //asigno la accion a la variable
        groundDetector = transform.Find("GroundDetector");
    }

    private void OnEnable()
    {
        playerJump.started += _ => Jump();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    // Update is called once per frame
    void Update()
    {
        MoveInputDetected();

        bool grounded = IsGrounded();
        playerAnimator.SetBool("Isjumping", !grounded);
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Jump()
    {
        // Solo puede saltar si está tocando el suelo
        if (!IsGrounded())
            return;

        // Aplicar salto
        rbPlayer.AddForce(new Vector2(0, 7f), ForceMode2D.Impulse);
        playerAudio.Play();
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapBox(
            groundDetector.position,
            new Vector2(0.3f, 0.1f),    // tamaño del BoxDetector
            0f,
            groundLayer
        );
    }
}
