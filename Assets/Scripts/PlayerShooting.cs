using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );
    }
}