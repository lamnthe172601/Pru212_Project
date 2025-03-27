using UnityEngine;
using UnityEngine.SceneManagement;

public class MainStory : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
    }
}
