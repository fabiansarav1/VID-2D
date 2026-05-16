using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject trophy;                // Meta
    public AudioSource victoryMusic;         // Música de victoria
    public GameObject winPanel;              // Panel YOU WIN

    // NUEVO
    public GameObject doorBlocked;           // Puerta cerrada
    public AudioSource doorOpenSFX;          // Sonido al abrir puerta

    private int coinsCollected = 0;
    private int totalCoins = 3;

    private PlayerBehaviour playerMovement;
    private Rigidbody2D playerRb;

    public AudioSource levelMusic;      // Música del nivel
    public AudioSource gameOverSFX;     // Sonido de Game Over

    void Start()
    {
        if (trophy != null)
            trophy.SetActive(false);

        // Buscar player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerBehaviour>();
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    public void AddCoin()
    {
        coinsCollected++;

        if (coinsCollected >= totalCoins)
        {
            // Mostrar puerta/meta
            if (trophy != null)
                trophy.SetActive(true);

            // Ocultar puerta cerrada
            if (doorBlocked != null)
                doorBlocked.SetActive(false);

            // Reproducir sonido de puerta abierta
            if (doorOpenSFX != null)
                doorOpenSFX.Play();
        }
    }

    public void PlayerReachedTrophy()
    {
        // Detener movimiento del jugador
        if (playerMovement != null)
            playerMovement.enabled = false;

        if (playerRb != null)
            playerRb.linearVelocity = Vector2.zero;

        // Activar panel de victoria
        if (winPanel != null)
            winPanel.SetActive(true);

        // Reproducir música de victoria
        if (victoryMusic != null)
            victoryMusic.Play();

        // Pausar juego
        Time.timeScale = 0f;

        Debug.Log("¡Nivel completado!");
    }
}