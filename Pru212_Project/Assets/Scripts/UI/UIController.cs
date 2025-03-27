using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Space(10)]
    public TMP_Text timeText;

    public ParticleSystem rainEffect;
    public GameObject lightningEffect;

    public GameObject gameOverUI;
    public TMP_Text timeSurvivedText;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gameOverUI.SetActive(false);
        CleanupDeathEffects(); // Clean up leftover effects from previous plays
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

    public void GameOver()
    {
        gameOverUI.SetActive(true);

        int minutes = Mathf.FloorToInt(Time.timeSinceLevelLoad / 60);
        int seconds = Mathf.FloorToInt(Time.timeSinceLevelLoad % 60);
        timeSurvivedText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        CleanupDeathEffects(); // Ensure all death effects are destroyed

        Time.timeScale = 0f;
    }

    private void CleanupDeathEffects()
    {
        GameObject[] effects = GameObject.FindGameObjectsWithTag("DeathEffect");
        foreach (GameObject effect in effects)
        {
            Destroy(effect);
        }
    }


    public void Title()
    {
        SceneManager.LoadScene("TitleScreen");
        Time.timeScale = 1.0f;
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void OnDestroy()
    {
        foreach (GameObject effect in GameObject.FindGameObjectsWithTag("DeathEffect"))
        {
            Destroy(effect);
        }
    }

}
