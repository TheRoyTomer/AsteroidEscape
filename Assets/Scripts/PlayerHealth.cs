using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingLives = 3;

    private int currentLives;

    private void Awake()
    {
        currentLives = startingLives;
    }

    public void TakeDamage()
    {
        currentLives--;

        Debug.Log("Lives remaining: " + currentLives);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            TakeDamage();
        }
    }
}