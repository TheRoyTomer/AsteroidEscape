using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private GameObject shieldEffect;
    [SerializeField] private GameObject shieldIcon;

    [SerializeField] private float shieldDuration = 15f;
    [SerializeField] private float blinkDuration = 2f;
    [SerializeField] private float blinkInterval = 0.15f;

    private bool hasShield;
    private bool isShieldActive;

    private int normalLayer;
    private int shieldedLayer;

    private void Awake()
    {
        shieldEffect.SetActive(false);
        shieldIcon.SetActive(false);

        normalLayer = gameObject.layer;
        shieldedLayer = LayerMask.NameToLayer("ShieldedPlayer");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ActivateShield();
        }
    }

    public void CollectShield()
    {
        hasShield = true;

        shieldIcon.SetActive(true);

        Debug.Log("Shield ready");
    }

    private void ActivateShield()
    {
        if (!hasShield || isShieldActive)
        {
            return;
        }

        hasShield = false;

        shieldIcon.SetActive(false);

        StartCoroutine(ShieldRoutine());
    }

    private IEnumerator ShieldRoutine()
    {
        isShieldActive = true;

        gameObject.layer = shieldedLayer;
        shieldEffect.SetActive(true);

        float waitBeforeBlink =
            Mathf.Max(0f, shieldDuration - blinkDuration);

        yield return new WaitForSeconds(waitBeforeBlink);

        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            shieldEffect.SetActive(!shieldEffect.activeSelf);

            yield return new WaitForSeconds(blinkInterval);

            elapsedTime += blinkInterval;
        }

        shieldEffect.SetActive(false);

        gameObject.layer = normalLayer;
        isShieldActive = false;
    }

    public bool IsShieldActive()
    {
        return isShieldActive;
    }

    public bool HasShield()
    {
        return hasShield;
    }
}