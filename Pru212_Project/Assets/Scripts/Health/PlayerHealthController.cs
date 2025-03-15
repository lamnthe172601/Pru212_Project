using System;
using UnityEditor.Rendering.LookDev;
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
            Time.timeScale = 0;            
        }
    }

    public void updateHp()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = Cu / Hp;
        }
    }

}
