using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Canvas panels to turn on and off
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject instructionsMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject controlsMenu;

    // Button sound
    [SerializeField] AudioSource clickSound;

    // Whenever the scene starts, the default layout will be active
    void Start()
    {
        instructionsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    // Plays a button sound, waits for 1 second, then plays the game level
    public void PlayGameButton(string name)
    {
        Invoke(nameof(PlayAudio), .5f);
        SceneManager.LoadScene(name);
    }

    // Plays a button sound, then changes the active canvas panel
    public void InstructionsMenuButton()
    {
        clickSound.Play();
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        instructionsMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    // Plays a button sound, then changes the active canvas panel
    public void MainMenuButton()
    {
        clickSound.Play();
        settingsMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    // Plays a button sound, then changes the active canvas panel
    public void SettingsMenuButton()
    {
        clickSound.Play();
        mainMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        settingsMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    // Plays a button sound, then exits the game
    public void QuitGameButton()
    {
        Invoke(nameof(PlayAudio), .5f);
        Application.Quit();
    }

    // Opens the controls menu
    public void ControlsMenu()
    {
        clickSound.Play();
        controlsMenu.SetActive(true);
        mainMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    // Method for Invoke
    void PlayAudio()
    {
        clickSound.Play();
    }
}
