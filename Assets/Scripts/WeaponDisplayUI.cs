using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplayUI : MonoBehaviour
{
    [SerializeField] private Sprite oneBulletSprite;
    [SerializeField] private Sprite twoBulletsSprite;
    [SerializeField] private Sprite threeBulletsSprite;

    private Image weaponImage;

    private void Awake()
    {
        weaponImage = GetComponent<Image>();
        SetWeaponDisplay(1);
    }

    public void SetWeaponDisplay(int weaponLevel)
    {
        if (weaponLevel == 1)
        {
            weaponImage.sprite = oneBulletSprite;
        }
        else if (weaponLevel == 2)
        {
            weaponImage.sprite = twoBulletsSprite;
        }
        else if (weaponLevel == 3)
        {
            weaponImage.sprite = threeBulletsSprite;
        }
    }
}