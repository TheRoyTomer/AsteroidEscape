using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private float spawnOffset = 1f;
    [SerializeField] private float spawnInterval = 7f;
    
    [SerializeField] private GameManagerScript gameManager;

    private Camera mainCamera;
    private float spawnTimer;
    
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        SpawnAsteroid();
    }
    
    private void Update()
    {
        if (!gameManager.IsGameActive())
        {
            return;
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnAsteroid();
            spawnTimer = 0f;
        }
    }
    
    
    private void SpawnAsteroid()
    {
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(
            new Vector3(0f, 0f, 0f)
        );

        Vector3 topRight = mainCamera.ViewportToWorldPoint(
            new Vector3(1f, 1f, 0f)
        );

        Vector2 spawnPosition = Vector2.zero;
        Vector2 movementDirection = Vector2.zero;

        int spawnSide = Random.Range(0, 4);

        switch (spawnSide)
        {
            case 0:
                spawnPosition = new Vector2(
                    topRight.x + spawnOffset,
                    Random.Range(bottomLeft.y, topRight.y)
                );
                movementDirection = Vector2.left;
                break;

            case 1:
                spawnPosition = new Vector2(
                    bottomLeft.x - spawnOffset,
                    Random.Range(bottomLeft.y, topRight.y)
                );
                movementDirection = Vector2.right;
                break;

            case 2:
                spawnPosition = new Vector2(
                    Random.Range(bottomLeft.x, topRight.x),
                    topRight.y + spawnOffset
                );
                movementDirection = Vector2.down;
                break;

            default:
                spawnPosition = new Vector2(
                    Random.Range(bottomLeft.x, topRight.x),
                    bottomLeft.y - spawnOffset
                );
                movementDirection = Vector2.up;
                break;
        }

        GameObject newAsteroid = Instantiate(
            asteroidPrefab,
            spawnPosition,
            Quaternion.identity
        );

        AsteroidController asteroidController =
            newAsteroid.GetComponent<AsteroidController>();

        asteroidController.Initialize(movementDirection);
    }
}