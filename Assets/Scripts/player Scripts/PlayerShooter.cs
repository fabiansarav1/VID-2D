using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public AudioSource shootSFX;

    public int bulletsLeft = 15;

    private InputAction shootAction;

    private void Awake()
    {
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    private void OnEnable()
    {
        shootAction.started += _ => Shoot();
    }

    void Shoot()
    {
        if (bulletsLeft <= 0)
            return;

        bulletsLeft--;

        GameObject bullet =
            Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.identity);

        int direction =
            transform.localScale.x > 0 ? 1 : -1;

        bullet.GetComponent<Bullet>()
            .SetDirection(direction);

        if (shootSFX != null)
            shootSFX.Play();
    }
}