using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Panel Pause Menu
    public Slider volumeSlider; // Thanh chỉnh âm lượng
    public Button muteButton;
    public Text muteButtonText; // Văn bản hiển thị trên nút Mute

    private bool isPaused = false; // Trạng thái game có bị Pause không
    private bool isMuted = false; // Trạng thái có bị Mute không

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Ban đầu ẩn menu pause
        pauseMenuUI.SetActive(false);

        // Kiểm tra xem các giá trị có tồn tại không, nếu không thì thiết lập mặc định
        if (!PlayerPrefs.HasKey("Muted")) PlayerPrefs.SetInt("Muted", 0);
        if (!PlayerPrefs.HasKey("Volume")) PlayerPrefs.SetFloat("Volume", 1f);

        // Lấy giá trị đã lưu
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;

        // Đảm bảo thanh trượt luôn hiển thị đúng giá trị khi khởi động
        volumeSlider.value = isMuted ? 0f : savedVolume;

        // Cập nhật âm thanh theo trạng thái hiện tại
        UpdateSound();

        // Gán sự kiện cho Slider và Nút Mute
        volumeSlider.onValueChanged.AddListener(SetVolume);
        muteButton.onClick.AddListener(ToggleMute);
    }



    // Update is called once per frame
    void Update()
    {
        // Nhấn ESC để bật/tắt Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f; // Dừng hoặc tiếp tục game
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);

        // Nếu âm lượng = 0 thì tự động chuyển sang trạng thái Muted
        isMuted = (volume == 0);
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);

        PlayerPrefs.Save();
        UpdateSound();
    }


    public void ToggleMute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            // Nếu mute, lưu giá trị hiện tại và đặt thanh trượt về 0
            PlayerPrefs.SetFloat("VolumeBeforeMute", volumeSlider.value);
            volumeSlider.value = 0;
        }
        else
        {
            // Nếu bỏ mute, khôi phục giá trị trước đó
            volumeSlider.value = PlayerPrefs.GetFloat("VolumeBeforeMute", 1f);
        }

        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
        UpdateSound();
    }

    private void UpdateSound()
    {
        AudioListener.volume = isMuted ? 0f : volumeSlider.value;
        muteButtonText.text = isMuted ? "🔇 Mute" : "🔊 Unmute";

        // Nếu đang bị mute thì thanh trượt = 0
        if (isMuted)
        {
            volumeSlider.value = 0;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("ChoiceCharacter"); 
        Time.timeScale = 1f;
    }
}
