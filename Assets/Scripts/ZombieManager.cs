using UnityEngine;
using System.Collections;

public class ZombieManager : MonoBehaviour
{
    // Holds the total/current number of zombies
    // and the max number of zombies
    public int totalZombies;
    private int maxZombies;

    // The current wave count
    private int waveCount;

    // The bool to tell us if we've reached the 
    // max number of zombies for the wave
    public bool spawnMaxReached;

    // Initialize variables to their starting values
    void Start()
    {
        waveCount = 1;
        spawnMaxReached = false;
    }

    void Update()
    {
        WaveCountUpdate();
        WaveManager();
    }

    // Updates the wave count based on the # of zombies
    void WaveCountUpdate()
    {
        if (totalZombies == maxZombies && !spawnMaxReached)
        {
            spawnMaxReached = true;
        }

        if (spawnMaxReached && totalZombies == 0)
        {
            spawnMaxReached = false;
            waveCount++;
        }
    }

    // Changes the maximum # of zombies based on the wave #
    void WaveManager()
    {
        switch (waveCount)
        {
            case 1:
                maxZombies = 30;
                break;
            case 2:
                maxZombies = 50;
                break;
            case 3:
                maxZombies = 75;
                break;
            case 4:
                maxZombies = 110;
                break;
            default:
                maxZombies = 30;
                break;
        }
    }
}
