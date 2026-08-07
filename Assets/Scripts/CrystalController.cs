using UnityEngine;
using System.Collections;

public class CrystalController : MonoBehaviour
{
    private enum CrystalType
    {
        Blue,
        Green
    }

    [SerializeField] private CrystalType crystalType;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip crystalCollectSound;

    [SerializeField] private GameObject disappearEffectPrefab;

    [SerializeField] private float lifeTime = 8f;
    [SerializeField] private float blinkDuration = 2f;
    [SerializeField] private float blinkInterval = 0.15f;

    private GameManagerScript gameManager;
    private bool wasCollected;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManagerScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(CrystalLifeRoutine());
    }

    private IEnumerator CrystalLifeRoutine()
    {
        float waitBeforeBlink =
            Mathf.Max(0f, lifeTime - blinkDuration);

        yield return new WaitForSeconds(waitBeforeBlink);

        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration && !wasCollected)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled =
                    !spriteRenderer.enabled;
            }

            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }

        if (!wasCollected)
        {
            CreateDisappearEffect();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (wasCollected || !other.CompareTag("Player"))
        {
            return;
        }

        wasCollected = true;

        if (crystalType == CrystalType.Blue)
        {
            gameManager.AddCrystal();
        }
        else
        {
            PlayerShield playerShield =
                other.GetComponent<PlayerShield>();

            if (playerShield != null)
            {
                playerShield.CollectShield();
            }
        }

        if (audioSource != null &&
            crystalCollectSound != null)
        {
            audioSource.PlayOneShot(crystalCollectSound);

            spriteRenderer.enabled = false;
            GetComponent<Collider2D>().enabled = false;

            Destroy(
                gameObject,
                crystalCollectSound.length
            );

            return;
        }

        Destroy(gameObject);
    }

    private void CreateDisappearEffect()
    {
        if (disappearEffectPrefab == null)
        {
            return;
        }

        Instantiate(
            disappearEffectPrefab,
            transform.position,
            Quaternion.identity
        );
    }
}