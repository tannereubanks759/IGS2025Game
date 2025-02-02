using System;
using UnityEngine;
using UnityEngine.VFX;

public class zombieSpawner : MonoBehaviour
{
    // The zombie prefabs
    [SerializeField] private GameObject[] zombies;

    // The game time
    private float gameTime;

    // range that the spawner will activate
    [SerializeField] private float spawnRange;

    // Player tracking
    private bool playerNear;
    private GameObject player;
    private float playerDistance;

    // The particles that play during spawning
    [SerializeField] VisualEffect spawnEffect;
    [SerializeField] private float spawnEffectLifetime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        IsPlayerNear();
        GameTimeManager();
    }

    // Time calculations
    void GameTimeManager()
    {
        gameTime += Time.deltaTime;

        if (gameTime > 5 && playerNear)
        {
            SpawnZombie(zombies);
        }
    }

    // Spawns a random zombie from the array, resets the game time
    void SpawnZombie(GameObject[] zombArray)
    {
        var index = UnityEngine.Random.Range(0, zombArray.Length);

        PlayParticles();

        GameObject Zombie = Instantiate(zombArray[index], this.transform.position, Quaternion.identity);

        gameTime = 0;
    }

    // Checks if the player is near
    void IsPlayerNear()
    {
        // calculate player distance
        playerDistance = (player.transform.position - this.transform.position).magnitude;

        // set variables
        if (playerDistance > spawnRange)
        {
            playerNear = false;
        }
        else
        {
            playerNear = true;
        }
    }

    // Play the particle system
    void PlayParticles()
    {
        var spawnPos = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);

        VisualEffect newParticleEffect = Instantiate(spawnEffect, spawnPos, Quaternion.identity);

        newParticleEffect.Play();

        Destroy(newParticleEffect.gameObject, spawnEffectLifetime);
    }
}
