using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExperience : MonoBehaviour
{
    public int level = 0;
    public int currentXP = 0;
    public int xpToNextLevel = 10;

    public Image xpBar; // Thanh kinh nghiệm (Fill Amount)
    public Text levelText; // Hiển thị level

    public void GainXP(int amount)
    {
        currentXP += amount;
        UpdateXPBar();
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            level++;

            // Công thức tăng XP yêu cầu mỗi level (tùy chỉnh nếu cần)
            xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.5f);

            Debug.Log($"Lên cấp {level}!");

            UpdateXPBar();
        }
    }

    private void UpdateXPBar()
    {
        if (xpBar != null)
        {
            xpBar.fillAmount = (float)currentXP / xpToNextLevel;
        }

        if (levelText != null)
        {
            levelText.text =  level.ToString();
        }
    }
}
