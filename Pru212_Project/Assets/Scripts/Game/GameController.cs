using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Space(10)]
    public float endTimer = 600f;

    private float timer;
    public bool gameActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the timer if the game is active
        if (gameActive == true)
        {
            timer += Time.deltaTime;
            UIController.instance.UpdateTimer(timer);
        }
    }
}
