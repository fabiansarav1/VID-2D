using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float parallaxMultiplier = 0.5f;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // Mover el fondo a distinta velocidad
        transform.position += new Vector3(deltaMovement.x * parallaxMultiplier, 
                                          deltaMovement.y * parallaxMultiplier, 
                                          0);

        lastCameraPosition = cameraTransform.position;
    }
}
