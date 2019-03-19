using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool paused = false;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Time.timeScale = 1.0f;
                pauseMenu.SetActive(false);
                paused = false;
            }
            else
            {
                Time.timeScale = 0.0f;
                pauseMenu.SetActive(true);
                paused = true;
            }
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
