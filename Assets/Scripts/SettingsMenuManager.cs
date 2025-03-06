using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    // Inspector references
    [SerializeField] private Slider masterVol, musicVol, sfxVol;
    [SerializeField] private AudioMixer mainAudioMixer;

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
}
