using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject PauseScreen;
    public bool isPaused;
    public KeyCode pauseKey;
    public FirstPersonController player;
    public GameObject DeathScreen;
    public GameObject playerUi;
    public bool isDead;
    void Start()
    {
        Resume();
        DeathScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseKey) && !isDead)
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
        playerUi.SetActive(true);
        isPaused = false;
        PauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
        Time.timeScale = 1f;
        player.cameraCanMove = true;
    }
    public void Pause()
    {
        playerUi.SetActive(false);
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
        LoadScene("MainMenu");
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void Die()
    {
        playerUi.SetActive(false);
        isDead = true;
        isPaused = false;
        PauseScreen.SetActive(false);
        DeathScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
        player.cameraCanMove = false;

    }

    public void RestartLevel()
    {
        Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
        Time.timeScale = 1f;
        LoadScene(SceneManager.GetActiveScene().name);
    }
}
