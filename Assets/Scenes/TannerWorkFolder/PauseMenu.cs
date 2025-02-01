using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject PauseScreen;
    public bool isPaused;
    public KeyCode pauseKey;
    public FirstPersonController player;
    void Start()
    {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
        PauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
        Time.timeScale = 1f;
        player.cameraCanMove = true;
    }
    public void Pause()
    {
        player.cameraCanMove = false;
        isPaused = true;
        PauseScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void ExitToMenu()
    {
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
