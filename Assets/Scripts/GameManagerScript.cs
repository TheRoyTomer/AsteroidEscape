using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private TMP_Text crystalText;
    [SerializeField] private PlayerShooting playerShooting;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip weaponUpgradeSound;

    private bool isGameActive = true;
    private int crystals = 0;
    private int currentWeaponLevel = 1;

    private void Awake()
    {
        crystalText.text = "Crystals: " + crystals;
        playerShooting.SetWeaponLevel(currentWeaponLevel);
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }

    public void GameOver()
    {
        isGameActive = false;

        DestroyAllAsteroids();

        Debug.Log("Game Over");
    }

    private void DestroyAllAsteroids()
    {
        GameObject[] asteroids =
            GameObject.FindGameObjectsWithTag("Asteroid");

        foreach (GameObject asteroid in asteroids)
        {
            Destroy(asteroid);
        }
    }

    public void AddCrystal()
    {
        crystals++;

        crystalText.text = "Crystals: " + crystals;

        int newWeaponLevel = 1;

        if (crystals >= 10)
        {
            newWeaponLevel = 3;
        }
        else if (crystals >= 5)
        {
            newWeaponLevel = 2;
        }

        if (newWeaponLevel != currentWeaponLevel)
        {
            currentWeaponLevel = newWeaponLevel;

            playerShooting.SetWeaponLevel(currentWeaponLevel);
            audioSource.PlayOneShot(weaponUpgradeSound);
        }

        Debug.Log("Crystals: " + crystals);
    }
}