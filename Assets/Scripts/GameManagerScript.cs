using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private TMP_Text crystalText;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private PlayerShooting playerShooting;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip weaponUpgradeSound;

    [SerializeField] private int asteroidScore = 1000;
    [SerializeField] private int blueCrystalScore = 500;
    [SerializeField] private int lifeLostPenalty = 2000;

    [SerializeField] private float multiplierIncreaseInterval = 10f;
    [SerializeField] private int maximumMultiplier = 10;
    
    private bool isGameActive = true;

    private int crystals = 0;
    private int currentWeaponLevel = 1;

    private int score = 0;
    private int scoreMultiplier = 1;

    private float multiplierTimer;

    private void Awake()
    {
        crystalText.text = crystals.ToString();

        playerShooting.SetWeaponLevel(currentWeaponLevel);

        UpdateScoreUI();
    }

    private void Update()
    {
        if (!isGameActive)
        {
            return;
        }

        multiplierTimer += Time.deltaTime;

        if (multiplierTimer >= multiplierIncreaseInterval)
        {
            multiplierTimer = 0f;

            if (scoreMultiplier < maximumMultiplier)
            {
                scoreMultiplier++;
            }
        }
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

        crystalText.text = crystals.ToString();

        AddScore(blueCrystalScore);

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

            audioSource.PlayOneShot(
                weaponUpgradeSound
            );
        }
    }

    public void AddAsteroidScore()
    {
        AddScore(asteroidScore);
    }

    public void RemoveLifeScore()
    {
        score -= lifeLostPenalty;

        if (score < 0)
        {
            score = 0;
        }

        UpdateScoreUI();
    }

    private void AddScore(int baseScore)
    {
        score += baseScore * scoreMultiplier;

        UpdateScoreUI();
    }
    
    public void AddScoreFromHit(int amount)
    {
        AddScore(amount);
    }

    private void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }
    
}