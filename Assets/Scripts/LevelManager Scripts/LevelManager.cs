using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject trophy;                // Referencia al Trophy (meta)
    public AudioSource victoryMusic;         // Música de victoria
    private int coinsCollected = 0;          // Contador de monedas
    private int totalCoins = 3;              // Total de monedas necesarias

    void Start()
    {
        if (trophy != null)
            trophy.SetActive(false); // Trophy oculto al inicio
    }

    public void AddCoin()
    {
        coinsCollected++;

        if (coinsCollected >= totalCoins)
        {
            // Activamos el Trophy cuando se recojan todas las monedas
            if (trophy != null)
                trophy.SetActive(true);
        }
    }

    public void PlayerReachedTrophy()
    {
        // Congelar el juego
        Time.timeScale = 0f;

        // Reproducir música de victoria
        if (victoryMusic != null)
            victoryMusic.Play();

        Debug.Log("¡Nivel completado!");
    }
}
