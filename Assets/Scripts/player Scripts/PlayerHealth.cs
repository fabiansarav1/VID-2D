using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;

    public Image[] heartsUI;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public GameObject gameOverPanel;

    // Panel negro para el fade de Game Over
    public Image fadePanelGO;
    public float fadeDuration = 0.8f;

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

        // Ignorar colisiones entre Player y Enemy
        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("Enemy"),
            true
        );

        animator.SetTrigger("Isdead");

        // Detener música del nivel
        if (levelMusic != null)
            levelMusic.Stop();

        // Sonido de Game Over
        if (gameOverSFX != null)
            gameOverSFX.Play();

        // Tiempo para ver la animación de muerte
        Invoke(nameof(ShowGameOverScreen), 1.2f);
    }

    private void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        float time = 0f;

        Color color = fadePanelGO.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            color.a = Mathf.Lerp(0f, 1f, time / fadeDuration);
            fadePanelGO.color = color;

            yield return null;
        }

        // Pausar todo el juego
        Time.timeScale = 0f;
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < heartsUI.Length; i++)
        {
            heartsUI[i].sprite = i < currentLives ? fullHeart : emptyHeart;
        }
    }
}