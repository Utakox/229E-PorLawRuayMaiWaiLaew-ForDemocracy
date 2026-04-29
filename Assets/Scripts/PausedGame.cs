using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PausedGame : MonoBehaviour
{
    public GameObject musicBG;

    public Canvas pausedCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pausedCanvas.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
            {
                pausedCanvas.gameObject.SetActive(true);
                musicBG.GetComponent<AudioSource>().Pause();
                Time.timeScale = 0f; // หยุดเกม
            }
            else
            {
                pausedCanvas.gameObject.SetActive(false);
                musicBG.GetComponent<AudioSource>().UnPause();
                Time.timeScale = 1f; // กลับมาเล่นต่อ
            }
        }
    }


}
