using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementForce = 10f;
    [SerializeField] private float maxSpeed = 5f;

    [SerializeField] private ParticleSystem engineEffect;
    [SerializeField] private AudioSource engineAudioSource;

    private Rigidbody2D rigidBody;
    private Vector2 movementInput;
    private Camera mainCamera;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        movementInput =
            new Vector2(horizontalInput, verticalInput).normalized;

        UpdateEngineEffect();
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(movementInput * movementForce);

        rigidBody.linearVelocity =
            Vector2.ClampMagnitude(
                rigidBody.linearVelocity,
                maxSpeed
            );

        RotateTowardsMovement();
        WrapAroundScreen();
    }

    private void RotateTowardsMovement()
    {
        if (movementInput == Vector2.zero)
        {
            return;
        }

        float angle =
            Mathf.Atan2(movementInput.y, movementInput.x)
            * Mathf.Rad2Deg - 90f;

        rigidBody.MoveRotation(angle);
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

        if (newPosition.x > topRight.x)
        {
            newPosition.x = bottomLeft.x;
        }
        else if (newPosition.x < bottomLeft.x)
        {
            newPosition.x = topRight.x;
        }

        if (newPosition.y > topRight.y)
        {
            newPosition.y = bottomLeft.y;
        }
        else if (newPosition.y < bottomLeft.y)
        {
            newPosition.y = topRight.y;
        }

        rigidBody.position = newPosition;
    }

    private void UpdateEngineEffect()
    {
        if (movementInput != Vector2.zero)
        {
            if (!engineEffect.isPlaying)
            {
                engineEffect.Play();
            }

            if (!engineAudioSource.isPlaying)
            {
                engineAudioSource.Play();
            }
        }
        else
        {
            if (engineEffect.isPlaying)
            {
                engineEffect.Stop(
                    true,
                    ParticleSystemStopBehavior.StopEmitting
                );
            }

            if (engineAudioSource.isPlaying)
            {
                engineAudioSource.Stop();
            }
        }
    }
}