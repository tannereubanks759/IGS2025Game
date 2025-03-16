using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuManager : MonoBehaviour
{
    // Inspector references for audio
    [SerializeField] private Slider masterVol, musicVol, sfxVol;
    [SerializeField] private AudioMixer mainAudioMixer;

    [SerializeField] private TMP_InputField mouseSens;
    public float defaultSens = 1f;

    // initialize the mouse sens settings
    // throws an error at line 24 but still works???
    void Start()
    {
        PlayerPrefs.SetFloat("MouseSense", defaultSens);
        PlayerPrefs.Save();

        if (PlayerPrefs.GetFloat("MouseSens") != 0f)
        {
            mouseSens.text = PlayerPrefs.GetFloat("MouseSens").ToString();
        }
        else
        {
            PlayerPrefs.SetFloat("MouseSens", defaultSens);
            mouseSens.text = PlayerPrefs.GetFloat("MouseSens").ToString();
            PlayerPrefs.Save();
        }
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

    // Changes the mouse sense when it is altered by the
    // player from the input field in settings menu
    // Throws an error for the TryParse line for some
    // reason yet it still works?
    public void ChangeMouseSens()
    {
        var mouseText = mouseSens.text;

        if (float.TryParse(mouseText, out float result))
        {
            PlayerPrefs.SetFloat("MouseSens", result);
        }

        PlayerPrefs.Save();
    }
}
