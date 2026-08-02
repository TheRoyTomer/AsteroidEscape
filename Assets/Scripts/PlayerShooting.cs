using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform firePointCenter;
    [SerializeField] private Transform firePointLeft;
    [SerializeField] private Transform firePointRight;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gunFireSound;


    private int weaponLevel = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    public void SetWeaponLevel(int newWeaponLevel)
    {
        weaponLevel = Mathf.Clamp(newWeaponLevel, 1, 3);
    }

    private void Shoot()
    {
        audioSource.PlayOneShot(gunFireSound, 0.25f);        
        if (weaponLevel == 1)
        {
            CreateBullet(firePointCenter);
        }
        else if (weaponLevel == 2)
        {
            CreateBullet(firePointLeft);
            CreateBullet(firePointRight);
        }
        else
        {
            CreateBullet(firePointLeft);
            CreateBullet(firePointCenter);
            CreateBullet(firePointRight);
        }
    }

    private void CreateBullet(Transform firePoint)
    {
        Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );
    }
}