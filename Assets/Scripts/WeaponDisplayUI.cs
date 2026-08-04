using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class WeaponDisplayUI : MonoBehaviour
{
    [SerializeField] private Sprite oneBulletSprite;
    [SerializeField] private Sprite twoBulletsSprite;
    [SerializeField] private Sprite threeBulletsSprite;

    private Image weaponImage;

    private void Awake()
    {
        weaponImage = GetComponent<Image>();
    }

    public void SetWeaponDisplay(int weaponLevel)
    {
        if (weaponImage == null)
        {
            weaponImage = GetComponent<Image>();
        }

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