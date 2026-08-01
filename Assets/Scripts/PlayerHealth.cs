using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingLives = 3;
    [SerializeField] private float invincibilityDuration = 1.5f;
    
    [SerializeField] private GameManagerScript gameManager;
    
    [SerializeField] private HealthUIScript healthUI;
    
    [SerializeField] private GameObject explosionPrefab;

    private int currentLives;
    private bool isInvincible;

    private void Awake()
    {
        currentLives = startingLives;
        healthUI.UpdateHealth(currentLives);
    }

    public void TakeDamage()
    {
        if (isInvincible)
        {
            return;
        }

        currentLives--;
        healthUI.UpdateHealth(currentLives);
        Debug.Log("Lives remaining: " + currentLives);
        
        if (currentLives <= 0)
        {
            GameOver();
            return;
        }
        
        isInvincible = true;
        Invoke(nameof(EndInvincibility), invincibilityDuration);
    }

    private void EndInvincibility()
    {
        isInvincible = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }
    
    private void GameOver()
    {
        gameManager.GameOver();

        Instantiate(
            explosionPrefab,
            transform.position,
            Quaternion.identity
        );

        gameObject.SetActive(false);
    }
}