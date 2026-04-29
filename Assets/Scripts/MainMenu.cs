using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas creditsCanvas;
    public Button closeCreditsButton;

    [Header("Music Settings")]
    public AudioSource bgMusic;
    public GameObject musicOnIcon;  // ลาก UI ไอคอนเพลงเปิดมาใส่
    public GameObject musicOffIcon; // ลาก UI ไอคอนเพลงปิดมาใส่

    private bool isMusicPlaying = true;

    void Start()
    {
        creditsCanvas.gameObject.SetActive(false);
        closeCreditsButton.gameObject.SetActive(false);

        // ตั้งค่าไอคอนเริ่มต้น
        musicOnIcon.SetActive(true);
        musicOffIcon.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("FirstScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenCredits()
    {
        creditsCanvas.gameObject.SetActive(true);
        closeCreditsButton.gameObject.SetActive(true);
    }

    public void buttonClosed()
    {
        creditsCanvas.gameObject.SetActive(false);
        closeCreditsButton.gameObject.SetActive(false);
    }

    public void ToggleMusic()
    {
        isMusicPlaying = !isMusicPlaying;

        if (isMusicPlaying)
        {
            bgMusic.Play();
            musicOnIcon.SetActive(true);
            musicOffIcon.SetActive(false);
        }
        else
        {
            bgMusic.Pause();
            musicOnIcon.SetActive(false);
            musicOffIcon.SetActive(true);
        }
    }
}