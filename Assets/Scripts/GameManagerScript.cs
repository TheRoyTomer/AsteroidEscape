using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    
    private bool isGameActive = true;
    private int score = 0;
    
    private void Awake()
    {
        scoreText.text = "Score: " + score;
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
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        foreach (GameObject asteroid in asteroids)
        {
            Destroy(asteroid);
        }
    }
    
    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;

        Debug.Log("Score: " + score);
    }
}
