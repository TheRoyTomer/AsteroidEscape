using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject howToPlayPanel;
    [SerializeField] private GameObject leaderboardPanel;
    [SerializeField] private TMP_Text leaderboardText;

    private void Update()
    {
        if (leaderboardPanel.activeSelf &&
            Input.anyKeyDown &&
            !Input.GetMouseButtonDown(0))
        {
            leaderboardPanel.SetActive(false);
        }
        
        if (howToPlayPanel.activeSelf && Input.anyKeyDown)
        {
            howToPlayPanel.SetActive(false);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OpenHowToPlay()
    {
        howToPlayPanel.SetActive(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
    
    public void OpenLeaderboard()
    {
        LeaderboardManager leaderboardManager =
            FindFirstObjectByType<LeaderboardManager>();

        LeaderboardData data =
            leaderboardManager.LoadLeaderboard();

        leaderboardText.text = "";

        for (int i = 0; i < data.scores.Count; i++)
        {
            leaderboardText.text +=
                (i + 1) + ". " +
                data.scores[i].ToString("N0") +
                "\n";
        }

        leaderboardPanel.SetActive(true);
    }
}