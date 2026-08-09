using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class LeaderboardData
{
    public List<int> scores = new List<int>();
}

public class LeaderboardManager : MonoBehaviour
{
    public static int LastScore { get; private set; }
    public static int LastRank { get; private set; }
    public static int HighScore { get; private set; }

    private string savePath;

    private void Awake()
    {
        savePath = Path.Combine(
            Application.persistentDataPath,
            "leaderboard.json"
        );
    }

    public void SaveScore(int newScore)
    {
        LeaderboardData data = LoadLeaderboard();

        LastScore = newScore;

        // מוסיפים את התוצאה החדשה
        data.scores.Add(newScore);

        // ממיינים מהגבוה לנמוך
        data.scores.Sort((a, b) => b.CompareTo(a));

        // המיקום האמיתי ברשימה
        LastRank = data.scores.IndexOf(newScore) + 1;

        // השיא הוא תמיד התוצאה הראשונה
        HighScore = data.scores[0];

        // שומרים רק את Top 10
        if (data.scores.Count > 10)
        {
            data.scores.RemoveRange(
                10,
                data.scores.Count - 10
            );
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public LeaderboardData LoadLeaderboard()
    {
        if (!File.Exists(savePath))
        {
            return new LeaderboardData();
        }

        string json =
            File.ReadAllText(savePath);

        LeaderboardData data =
            JsonUtility.FromJson<LeaderboardData>(json);

        if (data == null)
        {
            data = new LeaderboardData();
        }

        return data;
    }
}