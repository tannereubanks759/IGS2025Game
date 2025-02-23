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

    // references for the zombiemanager script
    private GameObject zombieManagerOBJ;
    private ZombieManager zombieManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the player
        player = GameObject.FindGameObjectWithTag("Player");

        // Get the reference to the spawn effect
        spawnEffect = GetComponentInChildren<VisualEffect>();

        // Find and get the zombie manager script reference
        zombieManagerOBJ = GameObject.FindGameObjectWithTag("ZombieManager");
        zombieManager = zombieManagerOBJ.GetComponent<ZombieManager>();
    }

    void Update()
    {
        // Update the can spawn variable
        CanSpawnUpdate();

        // If we can spawn then spawn
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
        // Initialize index
        int index;

        // Initialize the weighted spawn #
        var weightedSpawn = UnityEngine.Random.Range(0, 20);

        // The clown and female zombie need to be in the first and second
        // position in the array for this to work properly, they will spawn
        // half the time
        if (weightedSpawn <= 10)
        {
            index = UnityEngine.Random.Range(0, 1);
        }
        // This should be the position of the GS zombie
        else if (weightedSpawn <= 16)
        {
            index = 2;
        }
        // This is the cop zombie
        else
        {
            index = 3;
        }

        // Spawn a zombie
        GameObject Zombie = Instantiate(zombArray[index], this.transform.position, Quaternion.identity);

        // Increment the total # of zombies in the scene
        zombieManager.totalZombies++;

        // Either Andrew or Tanner did this part
        zombieAIV1 zombieScript = Zombie.GetComponent<zombieAIV1>();

        if (zombieScript != null)
        {
            zombieScript.SetMinigameScript(miniGameScript.instance);
        }
        
        // Reset the gameTime so the spawn-time resets
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

    // Update the can spawn variable
    void CanSpawnUpdate()
    {
        canSpawn = !zombieManager.spawnMaxReached;
    }
}
