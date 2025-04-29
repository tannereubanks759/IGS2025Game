using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioSource[] musicSources;
    public AudioClip[] parkMusic;
    public AudioClip buffMusic;
    public int currentSong;
    public GunHandler gunH;

    public PauseMenu pauseMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSong = Random.Range(0, parkMusic.Length);
        PlayNextSong();
        if(currentSong == 2)
        {
            PlayNextSong();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (musicSources[0].isPlaying == false && pauseMenu.isPaused != true)
        {
            PlayNextSong();
        }
        
    }

    public void PlayNextSong()
    {
        if(currentSong == parkMusic.Length - 1)
        {
            currentSong = 0;
        }
        else
        {
            currentSong++;
        }
        foreach(var source in musicSources)
        {
            source.clip = parkMusic[currentSong];
            source.Play();
        }
    }

    public void PlayBuffMusic()
    {
        foreach(var source in musicSources)
        {
            source.clip = buffMusic;
            source.Play();
        }
    }
}
