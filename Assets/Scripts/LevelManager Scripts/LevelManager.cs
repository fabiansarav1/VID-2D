using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public GameObject trophy;
    public AudioSource victoryMusic;
    public GameObject winPanel;

    public GameObject doorBlocked;
    public AudioSource doorOpenSFX;

    private int coinsCollected = 0;
    private int totalCoins = 3;

    private PlayerBehaviour playerMovement;
    private Rigidbody2D playerRb;

    public AudioSource levelMusic;
    public AudioSource gameOverSFX;

    // Fade
    public Image fadePanel;
    public float fadeDuration = 1f;

    void Start()
    {
        if (trophy != null)
            trophy.SetActive(false);

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        playerMovement = player.GetComponent<PlayerBehaviour>();
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    public void AddCoin()
    {
        coinsCollected++;

        if (coinsCollected >= totalCoins)
        {
            if (trophy != null)
                trophy.SetActive(true);

            if (doorBlocked != null)
                doorBlocked.SetActive(false);

            if (doorOpenSFX != null)
                doorOpenSFX.Play();
        }
    }

    public void PlayerReachedTrophy()
    {
        if (playerMovement != null)
            playerMovement.enabled = false;

        if (playerRb != null)
            playerRb.linearVelocity = Vector2.zero;

        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "SampleScene")
        {
            StartCoroutine(LoadLevel2());
        }
        else if (currentScene == "Level2")
        {
            StartCoroutine(GameComplete());
        }
    }

    IEnumerator LoadLevel2()
    {
        yield return StartCoroutine(FadeToBlack());

        SceneManager.LoadScene("Level2");
    }

    IEnumerator GameComplete()
    {
        if (winPanel != null)
            winPanel.SetActive(true);

        if (victoryMusic != null)
            victoryMusic.Play();

        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(FadeToBlack());

        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator FadeToBlack()
    {
        float timer = 0f;

        Color color = fadePanel.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);

            fadePanel.color = color;

            yield return null;
        }
    }
}