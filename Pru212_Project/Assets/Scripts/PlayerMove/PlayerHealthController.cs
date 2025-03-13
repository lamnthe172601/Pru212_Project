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

    void Awake()
    {
        instance = this;
    }
    void Start()
    {

        cu = hp;
        updateHp();
    }

    // Update is called once per frame
    void Update()
    {

    }  

    public void TakeDame(float dame)
    {
        cu -= dame;
        cu = Mathf.Max(cu, 0);
        updateHp();
        if (cu <= 0)
        {
            Time.timeScale = 0;            
        }
    }

    public void updateHp()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = cu / hp;
        }
    }

}
