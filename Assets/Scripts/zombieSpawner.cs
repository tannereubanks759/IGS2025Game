using System;
using UnityEngine;
using UnityEngine.VFX;

public class zombieSpawner : MonoBehaviour
{
    // The zombie prefabs
    [SerializeField] private GameObject[] zombies;
    [SerializeField] private float spawnRate;
    // The game time
    private float gameTime;

    // range that the spawner will activate
    [SerializeField] private float spawnRange;

    // Player tracking
    private bool playerNear;
    private GameObject player;
    private float playerDistance;

    // The particles that play during spawning
    private VisualEffect spawnEffect;

    // The bool to control if the spawners can spawn
    public static bool canSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnEffect = GetComponentInChildren<VisualEffect>();
    }

    void Update()
    {
        if (canSpawn)
        {
            IsPlayerNear();
            GameTimeManager();
        }
    }

    // Time calculations
    void GameTimeManager()
    {
        gameTime += Time.deltaTime;

        if (gameTime > spawnRate && playerNear)
        {
            SpawnZombie(zombies);
        }
    }

    // Spawns a random zombie from the array, resets the game time
    void SpawnZombie(GameObject[] zombArray)
    {
        var index = UnityEngine.Random.Range(0, zombArray.Length);

        GameObject Zombie = Instantiate(zombArray[index], this.transform.position, Quaternion.identity);

        zombieAIV1 zombieScript = Zombie.GetComponent<zombieAIV1>();

        if (zombieScript != null)
        {
            zombieScript.SetMinigameScript(miniGameScript.instance);
        }
        
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
            StopParticles();
        }
        else
        {
            playerNear = true;
            PlayParticles();
        }
    }

    // Play the particle system
    void PlayParticles()
    {
        spawnEffect.Play();

    }
    
    void StopParticles()
    {
        spawnEffect.Stop();
    }
}
