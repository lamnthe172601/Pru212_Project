using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Space(10)]
    public TMP_Text timeText;

    public ParticleSystem rainEffect;
    public GameObject lightningEffect;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        float time = Time.timeSinceLevelLoad;

        UpdateTimer(time);

        bool isEffectActive = Mathf.FloorToInt(time) % 20 >= 10;
        ToggleEffects(isEffectActive);
    }

    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60);

        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    private void ToggleEffects(bool state)
    {
        if (rainEffect != null)
        {
            if (state && !rainEffect.isPlaying)
            {
                rainEffect.Play();
            }
            else if (!state && rainEffect.isPlaying)
            {
                rainEffect.Stop(withChildren: true);
            }
        }

        if (lightningEffect != null)
        {
            lightningEffect.SetActive(state);
        }
    }
}
