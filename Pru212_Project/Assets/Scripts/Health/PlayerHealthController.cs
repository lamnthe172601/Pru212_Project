using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    // Singleton instance.
    public static PlayerHealthController instance;

    [SerializeField] private float hp = 100f;

    [SerializeField] private float cu;
    [SerializeField] private Image hpBar;

    public float Hp { get => hp; set => hp = value; }
    public float Cu { get => cu; set => cu = value; }

    void Awake()
    {
        instance = this;
    }
    void Start()
    {

        Cu = Hp;
        updateHp();      

    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void TakeDame(float dame)
    {
        Cu -= dame;
        Cu = Mathf.Max(Cu, 0);
        updateHp();

        if (Cu <= 0)
        {
            StartCoroutine(EliminateEnemiesBeforeGameOver());
        }
    }

    private IEnumerator EliminateEnemiesBeforeGameOver()
    {
        // Stop the player's movement
        GetComponent<PlayerMove>().enabled = false;

        // Keep damaging enemies and boss until none remain
        while (true)
        {
            EnemyController[] enemies = FindObjectsOfType<EnemyController>();
            BossController[] bosses = FindObjectsOfType<BossController>();

            if (enemies.Length == 0 && bosses.Length == 0)
                break; // All enemies are dead, show game over

            foreach (EnemyController enemy in enemies)
            {
                enemy.TakeDamage(1000f * Time.deltaTime);
            }

            foreach (BossController boss in bosses)
            {
                boss.TakeDamage(1000f * Time.deltaTime);
            }

            yield return null; // Wait for next frame
        }

        // Once all enemies are dead, show game over UI
        UIController.instance.GameOver();
        Time.timeScale = 0f; // Pause the game
    }


    public void Heal(int amount)
    {
        Cu += amount;
        Cu = Mathf.Min(Cu, Hp);
        updateHp();
    }

    public void updateHp()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = Cu / Hp;
        }
    }


}
