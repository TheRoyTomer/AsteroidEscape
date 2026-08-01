using UnityEngine;

public class HealthUIScript : MonoBehaviour
{
    [SerializeField] private GameObject[] lifeFills;

    public void UpdateHealth(int currentLives)
    {
        for (int i = 0; i < lifeFills.Length; i++)
        {
            lifeFills[i].SetActive(i < currentLives);
        }
    }
}