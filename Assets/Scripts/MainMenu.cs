using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Canvas panels to turn on and off
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject instructionsMenu;
    [SerializeField] GameObject settingsMenu;

    // Button sound
    //[SerializeField] AudioSource clickSound;

    // Whenever the scene starts, the default layout will be active
    void Start()
    {
        instructionsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    // Plays a button sound, waits for 1 second, then plays the game level
    public void PlayGameButton()
    {
        //Invoke(nameof(PlayAudio), 1);
        SceneManager.LoadScene("GameLevel");
    }

    // Plays a button sound, then changes the active canvas panel
    public void InstructionsMenuButton()
    {
        //clickSound.Play();
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        instructionsMenu.SetActive(true);
    }

    // Plays a button sound, then changes the active canvas panel
    public void MainMenuButton()
    {
        //clickSound.Play();
        settingsMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    // Plays a button sound, then changes the active canvas panel
    public void SettingsMenuButton()
    {
        //clickSound.Play();
        mainMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    // Plays a button sound, then exits the game
    public void QuitGameButton()
    {
        //Invoke(nameof(PlayAudio), 1);
        Application.Quit();
    }

    // Method for Invoke
    void PlayAudio()
    {
        //clickSound.Play();
    }
}
