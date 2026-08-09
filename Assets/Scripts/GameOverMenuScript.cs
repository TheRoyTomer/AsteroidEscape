using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenuScript : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text resultText;

    private void Start()
    {
        scoreText.text =
            LeaderboardManager.LastScore.ToString("N0");

        highScoreText.text =
            LeaderboardManager.HighScore.ToString("N0");

        if (LeaderboardManager.LastRank == 1)
        {
            resultText.text =
                "NEW HIGH SCORE!\nRANK #1";
        }
        else if (LeaderboardManager.LastRank <= 10)
        {
            resultText.text =
                "TOP 10!\nRANK #" +
                LeaderboardManager.LastRank;
        }
        else
        {
            resultText.text = "";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenuScene");
            return;
        }

        if (Input.anyKeyDown)
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }


}