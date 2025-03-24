using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ZombieManager : MonoBehaviour
{
    // Holds the total/current number of zombies
    // and the max number of zombies
    public int totalZombiesAlive;
    public int totalSpawnedZombies;
    private int maxZombies;

    // The current wave count
    private int waveCount;

    // The bool to tell us if we've reached the 
    // max number of zombies for the wave
    public bool spawnMaxReached;

    // The reference to the UI wave count
    public Text waveText;

    // reference for adding tickets
    public miniGameScript miniGameObject;
    [SerializeField] int ticketsOnCompletion;

    // holds the total number of zombies killed
    public int totalZombiesKilled = 0;

    //[SerializeField] GameObject scoreboardUI;

    // Initialize variables to their starting values
    void Start()
    {
        //scoreboardUI.SetActive(false);
        waveCount = 1;
        maxZombies = 12;
        spawnMaxReached = false;
        WaveTextUpdate();

        Debug.Log(zombieSpawner.spawnRate);
    }

    void Update()
    {
        WaveCountUpdate();
    }

    // Updates the wave count based on the # of zombies
    void WaveCountUpdate()
    {
        if (totalSpawnedZombies == maxZombies && !spawnMaxReached)
        {
            spawnMaxReached = true;
        }

        if (spawnMaxReached && totalZombiesAlive == 0)
        {
            spawnMaxReached = false;

            miniGameObject.tickets += ticketsOnCompletion;

            //WaveTextFadeOut();
            //new WaitForSeconds(1f);
            waveCount++;
            totalSpawnedZombies = 0;
            WaveManager();
            WaveTextUpdate();
           // WaveTextFadeIn();
        }
    }

    // Changes the maximum # of zombies allowed alive in the level at one time
    void MaxZombiesAllowedUpdate()
    {
        zombieSpawner.maxAliveZombies += 5;
    }

    void SpawnRateUpdate()
    {
        if (zombieSpawner.spawnRate >= 1f)
        {
            zombieSpawner.spawnRate -= 1;
        }

        Debug.Log(zombieSpawner.spawnRate);
    }

    // Changes the maximum # of zombies based on the wave #
    void WaveManager()
    {
        maxZombies += 5 + (3 * (waveCount - 2));

        MaxZombiesAllowedUpdate();

        SpawnRateUpdate();
    }

    void WaveTextUpdate()
    {
        waveText.text = waveCount.ToString();
    }

    void WaveTextFadeOut()
    {
        waveText.CrossFadeAlpha(0f, 1f, false);
    }

    void WaveTextFadeIn()
    {
        waveText.CrossFadeAlpha(1f, 1f, false);
    }
}
