using UnityEngine;

public class CrystalController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip crystalCollectSound;
    
    private GameManagerScript gameManager;
    private bool wasCollected;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManagerScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (wasCollected || !other.CompareTag("Player"))
        {
            return;
        }

        wasCollected = true;
        gameManager.AddCrystal();

        audioSource.PlayOneShot(crystalCollectSound);

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, crystalCollectSound.length);
    }
}