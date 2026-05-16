using UnityEngine;
using UnityEngine.Rendering.Universal; // Necesario para Light2D

public class Recollectable : MonoBehaviour
{
    private LevelManager levelManager;
    private Light2D coinLight;

    void Start()
    {
        levelManager = FindFirstObjectByType<LevelManager>();

        // Busca automáticamente la luz en el hijo
        coinLight = GetComponentInChildren<Light2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Oculta sprite y colisión
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            // Apaga la luz si existe
            if (coinLight != null)
                coinLight.enabled = false;

            // Sonido
            GetComponent<AudioSource>().Play();

            // Notifica al LevelManager
            if (levelManager != null)
                levelManager.AddCoin();

            // Destruir después del sonido
            Destroy(gameObject, 3f);
        }
    }
}
