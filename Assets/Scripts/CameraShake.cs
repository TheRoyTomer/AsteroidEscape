using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPosition;
    private Coroutine shakeCoroutine;

    private void Awake()
    {
        originalPosition = transform.localPosition;
    }

    public void Shake(float duration, float strength)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            transform.localPosition = originalPosition;
        }

        shakeCoroutine = StartCoroutine(
            ShakeRoutine(duration, strength)
        );
    }

    private IEnumerator ShakeRoutine(
        float duration,
        float strength
    )
    {
        float elapsedTime = 0f;
        float shakeInterval = 0.04f;

        while (elapsedTime < duration)
        {
            float remainingStrength =
                strength * (1f - elapsedTime / duration);

            float offsetX = Random.Range(
                -remainingStrength,
                remainingStrength
            );

            float offsetY = Random.Range(
                -remainingStrength,
                remainingStrength
            );

            transform.localPosition =
                originalPosition +
                new Vector3(offsetX, offsetY, 0f);

            yield return new WaitForSecondsRealtime(
                shakeInterval
            );

            elapsedTime += shakeInterval;
        }

        transform.localPosition = originalPosition;
        shakeCoroutine = null;
    }
}