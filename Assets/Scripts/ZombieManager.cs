using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ZombieManager : MonoBehaviour
{
    // Holds the total/current number of zombies
    // and the max number of zombies
    public static int totalZombiesAlive;
    public static int totalSpawnedZombies;
    private int maxZombies;

    // The current wave count
    public static int waveCount;

    // The bool to tell us if we've reached the 
    // max number of zombies for the wave
    public static bool spawnMaxReached;

    // The reference to the UI wave count
    public Text waveText;

    // reference for adding tickets
    public miniGameScript miniGameObject;
    [SerializeField] int ticketsOnCompletion;

    // holds the total number of zombies killed
    public static int totalZombiesKilled = 0;

    //[SerializeField] GameObject scoreboardUI;

    // Initialize variables to their starting values
    void Start()
    {
        //scoreboardUI.SetActive(false);
        waveCount = 1;
        maxZombies = 12;
        spawnMaxReached = false;
        totalZombiesKilled = 0;
        waveCount = 1;
        totalZombiesAlive = 0;
        totalSpawnedZombies = 0;
        WaveTextUpdate();

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
        if (zombieSpawner.spawnRate > 3f)
        {
            zombieSpawner.spawnRate -= 1;
        }

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
