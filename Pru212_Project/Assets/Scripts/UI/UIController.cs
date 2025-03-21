using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Space(10)]
    // UI elements for displaying time
    public TMP_Text timeText;

    public GameObject rainEffect;
    public GameObject lightningEffect; 

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        float time = Time.time; 

        // Update the timer UI
        UpdateTimer(time);

        // Activate rain & thunder for 10s, then deactivate for 10s
        bool isEffectActive = Mathf.FloorToInt(time) % 20 > 10;
        ToggleEffects(isEffectActive);
    }

    // Update the timer display
    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60);

        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    private void ToggleEffects(bool state)
    {
        rainEffect.SetActive(state);
        lightningEffect.SetActive(state);
    }
}
