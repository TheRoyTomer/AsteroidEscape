using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float wrapMargin = 2f;

    private Rigidbody2D rigidBody;
    private Camera mainCamera;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    public void Initialize(Vector2 movementDirection)
    {
        rigidBody.linearVelocity =
            movementDirection.normalized * movementSpeed;

        rigidBody.angularVelocity = rotationSpeed;
    }
    
    private void FixedUpdate()
    {
        WrapAroundScreen();
    }
    
    private void WrapAroundScreen()
    {
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(
            new Vector3(0f, 0f, 0f)
        );

        Vector3 topRight = mainCamera.ViewportToWorldPoint(
            new Vector3(1f, 1f, 0f)
        );

        Vector2 newPosition = rigidBody.position;

        if (newPosition.x > topRight.x + wrapMargin)
        {
            newPosition.x = bottomLeft.x - wrapMargin;
        }
        else if (newPosition.x < bottomLeft.x - wrapMargin)
        {
            newPosition.x = topRight.x + wrapMargin;
        }

        if (newPosition.y > topRight.y + wrapMargin)
        {
            newPosition.y = bottomLeft.y - wrapMargin;
        }
        else if (newPosition.y < bottomLeft.y - wrapMargin)
        {
            newPosition.y = topRight.y + wrapMargin;
        }
        rigidBody.position = newPosition;
    }
}