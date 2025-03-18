﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private int currentXP = 0;
    [SerializeField] private int xpToNextLevel = 100;
    [SerializeField] private int level = 1;
    [SerializeField] private Image xpBar;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private List<Button> upgradeButtons;
    [SerializeField] private List<Image> upgradeIcons; // Danh sách ảnh nâng cấp
    [SerializeField] private List<TextMeshProUGUI> upgradeDescriptions; // Mô tả nâng cấp
    [SerializeField] private Sprite damageIcon, bulletIcon, speedIcon, magnetIcon; // Ảnh của từng nâng cấp

    private List<int> availableUpgrades = new List<int> { 1, 2, 3, 4 };

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
            xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.5f);
            Time.timeScale = 0f;
            ShowUpgradeOptions();
        }
    }

    private void ShowUpgradeOptions()
    {
        upgradePanel.SetActive(true);
        List<int> chosenUpgrades = GetRandomUpgrades(3);

        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            int upgradeType = chosenUpgrades[i];
            upgradeButtons[i].onClick.RemoveAllListeners();
            upgradeButtons[i].onClick.AddListener(() => SelectUpgrade(upgradeType));

            upgradeDescriptions[i].text = GetUpgradeText(upgradeType);
            upgradeIcons[i].sprite = GetUpgradeIcon(upgradeType);
        }
    }

    private Sprite GetUpgradeIcon(int type)
    {
        switch (type)
        {
            case 1: return damageIcon;
            case 2: return bulletIcon;
            case 3: return speedIcon;
            case 4: return magnetIcon;
            default: return null;
        }
    }


    private List<int> GetRandomUpgrades(int count)
    {
        List<int> shuffled = new List<int>(availableUpgrades);
        for (int i = 0; i < shuffled.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffled.Count);
            (shuffled[i], shuffled[randomIndex]) = (shuffled[randomIndex], shuffled[i]);
        }
        return shuffled.GetRange(0, Mathf.Min(count, shuffled.Count));
    }

    private string GetUpgradeText(int type)
    {
        switch (type)
        {
            case 1: return "Increase damage";
            case 2: return "Increase bullet traces";
            case 3: return "Increase running speed";
            case 4: return "Absorb all Exp & HP";
            default: return "Unknown upgrade";

        }
    }

    private void SelectUpgrade(int upgradeType)
    {
        switch (upgradeType)
        {
            case 1:
                FindObjectOfType<MoveGun>().IncreaseDamage();
                break;
            case 2:
                FindObjectOfType<MoveGun>().IncreaseBulletCount();
                break;
            case 3:
                FindObjectOfType<PlayerMove>().IncreaseMoveSpeed();
                break;
            case 4:
                FindObjectOfType<PlayerMove>().magnetChance += 0.5f;
                FindObjectOfType<PlayerMove>().ActivateMagnetChance();
                break;
        }
        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void UpdateXPBar()
    {
        xpBar.fillAmount = (float)currentXP / xpToNextLevel;
    }
}
