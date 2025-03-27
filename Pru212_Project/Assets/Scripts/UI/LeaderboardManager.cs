using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreEntry
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class Leaderboard
{
    public List<ScoreEntry> scores = new List<ScoreEntry>();
}

public class LeaderboardManager : MonoBehaviour
{
    private const string LeaderboardKey = "LeaderboardData";
    public List<ScoreEntry> highScores = new List<ScoreEntry>();

    private void Start()
    {
        LoadLeaderboard();
    }

    public void AddScore(string playerName, int score)
    {
        highScores.Add(new ScoreEntry { playerName = playerName, score = score });
        highScores.Sort((a, b) => b.score.CompareTo(a.score)); // Sort highest to lowest

        if (highScores.Count > 10) // Limit to top 10 scores
        {
            highScores.RemoveAt(highScores.Count - 1);
        }

        SaveLeaderboard();
    }

    private void SaveLeaderboard()
    {
        string json = JsonUtility.ToJson(new Leaderboard { scores = highScores });
        PlayerPrefs.SetString(LeaderboardKey, json);
        PlayerPrefs.Save();
    }

    private void LoadLeaderboard()
    {
        if (PlayerPrefs.HasKey(LeaderboardKey))
        {
            string json = PlayerPrefs.GetString(LeaderboardKey);
            highScores = JsonUtility.FromJson<Leaderboard>(json).scores;
        }
    }
}
