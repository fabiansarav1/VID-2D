using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Referencia al transform del jugador
    public float smoothing = 5f;  // Velocidad de suavizado del movimiento de la cámara

    private Vector3 offset;  // Distancia entre la cámara y el jugador

    void Start()
    {
        // Calculamos el offset al inicio
        offset = transform.position - player.position;
    }

    void FixedUpdate()
    {
        // Nueva posición de la cámara, tomando en cuenta el offset
        Vector3 targetCamPos = player.position + offset;
        
        // Movemos la cámara suavemente hacia la posición deseada
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}

