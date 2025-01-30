using System;
using UnityEngine;

public class zombieSpawner : MonoBehaviour
{
    // The zombie prefabs
    [SerializeField] GameObject[] zombies;

    // The spawnpoints
    GameObject[] zombieSpawns;
    float[] spawnDistance;

    // The player
    GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Populate the spawn array with the spawns
        zombieSpawns = GameObject.FindGameObjectsWithTag("Spawner");

        // Get the player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        SpawnerDistnaceCalculation();
        SpawnerActivation();
    }


    // Function to set the 5 closest spawners active
    void SpawnerDistnaceCalculation()
    {
        // Populate the array to hold the distance between the player and the spawners
        for (int i = 0; i < zombieSpawns.Length; i++)
        {
            spawnDistance[i] = (player.transform.position - zombieSpawns[i].transform.position).magnitude;
        }

        Array.Sort(spawnDistance);
    }

    // Activate the 5 closest spawners, deactiveate the others
    void SpawnerActivation()
    {
        for (int i = 0; i < spawnDistance.Length; i++)
        {
            if (i < 5)
            {
                zombieSpawns[i].SetActive(true);
            }
            else
            {
                zombieSpawns[i].SetActive(false);
            }
        }
    }
}
