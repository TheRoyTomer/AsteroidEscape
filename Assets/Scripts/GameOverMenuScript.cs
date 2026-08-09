using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameOverMenuScript : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text resultText;

    [SerializeField] private float blinkInterval = 0.4f;

    private void Start()
    {
        scoreText.text = LeaderboardManager.LastScore.ToString("N0");
        highScoreText.text = LeaderboardManager.HighScore.ToString("N0");

        if (LeaderboardManager.LastRank == 1 &&
            LeaderboardManager.LastScore == LeaderboardManager.HighScore)
        {
            resultText.text = "NEW HIGH SCORE!  RANK #1";
        }
        else if (LeaderboardManager.LastRank <= 10)
        {
            resultText.text = "TOP 10!  RANK #" + LeaderboardManager.LastRank;
        }
        else
        {
            resultText.text = "";
        }

        resultText.enableWordWrapping = false;
        resultText.overflowMode = TextOverflowModes.Overflow;

        if (!string.IsNullOrEmpty(resultText.text))
        {
            StartCoroutine(BlinkResultText());
        }
    }

    private IEnumerator BlinkResultText()
    {
        while (true)
        {
            resultText.enabled = !resultText.enabled;
            yield return new WaitForSecondsRealtime(blinkInterval);        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GoToMainMenu();
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

    private void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
}