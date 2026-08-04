using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private float spawnOffset = 1f;
    [SerializeField] private float spawnInterval = 2.5f;

    [SerializeField] private float difficultyIncreaseInterval = 7f;
    [SerializeField] private float spawnIntervalDecrease = 0.3f;
    [SerializeField] private float minimumSpawnInterval = 0.5f;

    [SerializeField] private GameManagerScript gameManager;

    private Camera mainCamera;
    private float spawnTimer;
    private float difficultyTimer;

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

        difficultyTimer += Time.deltaTime;

        if (difficultyTimer >= difficultyIncreaseInterval)
        {
            spawnInterval = Mathf.Max(
                minimumSpawnInterval,
                spawnInterval - spawnIntervalDecrease
            );

            difficultyTimer = 0f;

            Debug.Log("New spawn interval: " + spawnInterval);
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

        int spawnSide = Random.Range(0, 4);

        switch (spawnSide)
        {
            case 0:
                spawnPosition = new Vector2(
                    topRight.x + spawnOffset,
                    Random.Range(bottomLeft.y, topRight.y)
                );
                break;

            case 1:
                spawnPosition = new Vector2(
                    bottomLeft.x - spawnOffset,
                    Random.Range(bottomLeft.y, topRight.y)
                );
                break;

            case 2:
                spawnPosition = new Vector2(
                    Random.Range(bottomLeft.x, topRight.x),
                    topRight.y + spawnOffset
                );
                break;

            default:
                spawnPosition = new Vector2(
                    Random.Range(bottomLeft.x, topRight.x),
                    bottomLeft.y - spawnOffset
                );
                break;
        }

        Vector2 targetPosition = new Vector2(
            Random.Range(bottomLeft.x, topRight.x),
            Random.Range(bottomLeft.y, topRight.y)
        );

        Vector2 movementDirection =
            (targetPosition - spawnPosition).normalized;

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