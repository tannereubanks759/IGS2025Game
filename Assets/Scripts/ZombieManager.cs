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
    public TextMeshPro waveText;

    // Initialize variables to their starting values
    void Start()
    {
        waveCount = 1;
        maxZombies = 30;
        spawnMaxReached = false;
        WaveTextUpdate();
    }

    void Update()
    {
        //WaveManager();
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
            
            waveCount++;
            totalSpawnedZombies = 0;
            WaveManager();
            WaveTextUpdate();
        }
    }

    // Changes the maximum # of zombies based on the wave #
    void WaveManager()
    {
        maxZombies += 20 + (5 * (waveCount - 2));
    }

    void WaveTextUpdate()
    {
        waveText.text = waveCount.ToString();
    }
}
