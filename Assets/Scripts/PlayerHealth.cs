using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingLives = 3;
    [SerializeField] private float invincibilityDuration = 1.5f;
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private float blinkInterval = 0.15f;

    [SerializeField] private GameManagerScript gameManager;
    [SerializeField] private HealthUIScript healthUI;
    [SerializeField] private GameObject explosionPrefab;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shipImpactSound;

    private int currentLives;
    private bool isInvincible;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();

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

        audioSource.PlayOneShot(shipImpactSound);

        isInvincible = true;
        StartCoroutine(BlinkDuringInvincibility());
    }

    private IEnumerator BlinkDuringInvincibility()
    {
        float elapsedTime = 0f;

        while (elapsedTime < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(blinkInterval);

            elapsedTime += blinkInterval;
        }

        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Asteroid"))
        {
            return;
        }

        Vector2 knockbackDirection =
            ((Vector2)transform.position -
             (Vector2)collision.transform.position).normalized;

        rigidBody.AddForce(
            knockbackDirection * knockbackForce,
            ForceMode2D.Impulse
        );

        TakeDamage();

        AsteroidController asteroidController =
            collision.gameObject.GetComponent<AsteroidController>();

        if (asteroidController != null)
        {
            asteroidController.Explode(false);
        }
    }

    private void GameOver()
    {
        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        gameManager.GameOver();

        ParticleSystem engineEffect =
            GetComponentInChildren<ParticleSystem>();

        if (engineEffect != null)
        {
            engineEffect.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear
            );
        }

        AudioSource engineAudioSource =
            engineEffect != null
                ? engineEffect.GetComponent<AudioSource>()
                : null;

        if (engineAudioSource != null)
        {
            engineAudioSource.Stop();
        }

        Instantiate(
            explosionPrefab,
            transform.position,
            Quaternion.identity
        );

        spriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        rigidBody.linearVelocity = Vector2.zero;
        rigidBody.angularVelocity = 0f;

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("GameOverScene");
    }
}