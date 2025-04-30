using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseScreen;
    public GameObject SettingsScreen;
    public bool isPaused;
    public KeyCode pauseKey;
    public FirstPersonController player;
    public GameObject DeathScreen;
    public CanvasGroup playerUi;
    public bool isDead;

    // audio control
    [SerializeField] private Slider masterVol, musicVol, sfxVol;
    [SerializeField] private AudioMixer mainAudioMixer;

    // Endgame Stats
    [SerializeField] private GameObject endgameStats;

    // mouse sens control
    public TMP_InputField mouseSens;
    public float defaultSens = 1f;

    // background blur
    public Volume globalVolume;
    private ControlBlur controlBlur;

    public GameObject[] speakers;

    public GameObject winScreen;

    private void Start()
    {
        Resume();
        DeathScreen.SetActive(false);

        if (mainAudioMixer != null)
        {
            // when the game starts we set the audio values from the playerprefs
            mainAudioMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
            mainAudioMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
            mainAudioMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
        }

        // when the game starts we set the mouse sens from playerprefs
        if (PlayerPrefs.GetFloat("MouseSens") != 0f)
        {
            player.mouseSensitivity = PlayerPrefs.GetFloat("MouseSens");
            mouseSens.text = PlayerPrefs.GetFloat("MouseSens").ToString();
        }
        else
        {
            player.mouseSensitivity = defaultSens;
        }

        masterVol.value = PlayerPrefs.GetFloat("MasterVol");
        musicVol.value = PlayerPrefs.GetFloat("MusicVol");
        sfxVol.value = PlayerPrefs.GetFloat("SFXVol");

        controlBlur = globalVolume.GetComponent<ControlBlur>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseKey) && !isDead && !winScreen.activeSelf)
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
        playerUi.alpha = 1f;
        isPaused = false;
        PauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
        Time.timeScale = 1f;
        player.cameraCanMove = true;
        if (controlBlur != null)
        {
            controlBlur.ToggleBackgroundBlur();
        }

        foreach (GameObject s in speakers)
        {
            s.GetComponent<AudioSource>().UnPause();
        }

    }

    public void ResumeFromSettings()
    {
        playerUi.alpha = 1f;
        isPaused = false;
        SettingsScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
        Time.timeScale = 1f;
        player.cameraCanMove = true;
        controlBlur.ToggleBackgroundBlur();

        foreach (GameObject s in speakers)
        {
            s.GetComponent<AudioSource>().UnPause();
        }

    }
    public void Pause()
    {
        //commented out to fix ui reseting animation
        //playerUi.SetActive(false);
        playerUi.alpha = 0f;
        player.cameraCanMove = false;
        isPaused = true;
        PauseScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
        controlBlur.ToggleBackgroundBlur();

        foreach(GameObject s in speakers)
        {
            s.GetComponent<AudioSource>().Pause();
        }

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
        playerUi.alpha = 0f;
        isDead = true;
        isPaused = false;
        PauseScreen.SetActive(false);
        DeathScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
        player.cameraCanMove = false;

        endgameStats.GetComponent<EndgameStats>().StatsUpdate();

    }

    public void RestartLevel()
    {
        Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
        Time.timeScale = 1f;
        LoadScene(SceneManager.GetActiveScene().name);

        ZombieManager.totalZombiesKilled = 0;
        ticketGiverScript.totTicketsGained = 0;
    }

    public void SettingsMenu()
    {
        PauseScreen.SetActive(false);
        SettingsScreen.SetActive(true);
    }

    public void PausefromSettings()
    {
        SettingsScreen.SetActive(false);
        PauseScreen.SetActive(true);
    }

    // Change the master volume
    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterVol", masterVol.value);
        PlayerPrefs.SetFloat("MasterVol", masterVol.value);
        PlayerPrefs.Save();
    }

    // Change the music volume
    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVol", musicVol.value);
        PlayerPrefs.SetFloat("MusicVol", musicVol.value);
        PlayerPrefs.Save();
    }

    // Change the SFX volume
    public void ChangeSFXVolume()
    {
        mainAudioMixer.SetFloat("SFXVol", sfxVol.value);
        PlayerPrefs.SetFloat("SFXVol", sfxVol.value);
        PlayerPrefs.Save();
    }

    // Change the mouse sens
    public void ChangeMouseSens()
    {
        
        try
        {
            PlayerPrefs.SetFloat("MouseSens", float.Parse(mouseSens.text));
            PlayerPrefs.Save();

            player.mouseSensitivity = PlayerPrefs.GetFloat("MouseSens");
        }
        catch 
        {
            player.mouseSensitivity = defaultSens;
        }
    }
}
