using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 20f;
    [SerializeField] private float lifeTime = 7f;

    private Rigidbody2D rigidBody;
    
    private GameManagerScript gameManager;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        gameManager = FindFirstObjectByType<GameManagerScript>();
    }

    private void Start()
    {
        rigidBody.linearVelocity = transform.up * movementSpeed;

        Destroy(gameObject, lifeTime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            gameManager.AddScore(10);
            AsteroidController asteroidController =
                other.GetComponent<AsteroidController>();

            asteroidController.Explode();
            Destroy(gameObject);
            
        }
    }
}