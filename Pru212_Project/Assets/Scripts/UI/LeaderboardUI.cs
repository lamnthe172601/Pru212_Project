using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    public TMP_Text leaderboardText;
    public LeaderboardManager leaderboardManager;

    private void Start()
    {
        UpdateLeaderboard();
    }

    public void UpdateLeaderboard()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var entry in leaderboardManager.highScores)
        {
            sb.AppendLine($"{entry.playerName}: {entry.score}");
        }
        leaderboardText.text = sb.ToString();
    }
}
