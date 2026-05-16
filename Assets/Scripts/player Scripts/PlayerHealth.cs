using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;

    public Image[] heartsUI;     
    public Sprite fullHeart;     
    public Sprite emptyHeart;

    public GameObject gameOverPanel;

    public AudioSource levelMusic;      // Música del nivel
    public AudioSource gameOverSFX;     // Sonido de muerte

    private Animator animator;
    private PlayerBehaviour playerMovement; 
    private PlayerRespawn respawnScript;

    private bool isDead = false;

    private void Start()
    {
        currentLives = maxLives;
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerBehaviour>();
        respawnScript = GetComponent<PlayerRespawn>();

        UpdateHeartsUI();
    }

    public void TakeDamage()
    {
        if (isDead) return;

        currentLives--;
        UpdateHeartsUI();

        if (currentLives <= 0)
        {
            Die();
        }
        else
        {
            respawnScript.Respawn();
        }
    }

    private void Die()
    {
        isDead = true;

        playerMovement.enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        animator.SetTrigger("Isdead");

        // Detener música del nivel
        if (levelMusic != null)
            levelMusic.Stop();

        // Sonido de Game Over
        if (gameOverSFX != null)
            gameOverSFX.Play();

        Invoke(nameof(ShowGameOverScreen), 1f);
    }

    private void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < heartsUI.Length; i++)
        {
            heartsUI[i].sprite = i < currentLives ? fullHeart : emptyHeart;
        }
    }
}
