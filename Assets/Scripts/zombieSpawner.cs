using System;
using System.Linq;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.VFX;

public class zombieSpawner : MonoBehaviour
{
    // The zombie prefabs
    [SerializeField] private GameObject[] zombies;

    // The spawn rate of the spawners
    public static float spawnRate = 6f;

    // The game time
    private float gameTime;

    // range that the spawner will activate
    [SerializeField] private float spawnRange;

    // Player tracking
    private bool playerNear;
    private GameObject player;
    private float playerDistance;

    //// The particles that play during spawning
    //private VisualEffect spawnEffect;

    // The bool to control if the spawners can spawn
    public static bool canSpawn;

    // references for the zombiemanager script
    private GameObject zombieManagerOBJ;
    private ZombieManager zombieManager;

    // the maximum number of zombies allowed alive at one time
    public static int maxAliveZombies;

    // audio control
    private AudioSource spawnerSource;

    // debug gizmo tool
    float alpha = .2f;

    //public static zombieSpawner closestSpawner;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the player
        player = GameObject.FindGameObjectWithTag("Player");

        //// Get the reference to the spawn effect
        //spawnEffect = GetComponentInChildren<VisualEffect>();

        // Find and get the zombie manager script reference
        zombieManagerOBJ = GameObject.FindGameObjectWithTag("ZombieManager");
        zombieManager = zombieManagerOBJ.GetComponent<ZombieManager>();

        // the starting value of the max zombies alive
        maxAliveZombies = 8;

        spawnerSource = GetComponent<AudioSource>();

        //closestSpawner = this;
        //StopParticles();
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
        if (playerNear /*&& closestSpawner == this*/)
        {
            gameTime += Time.deltaTime;

            if (gameTime > spawnRate)
            {
                //PlayParticles();
                SpawnZombie(zombies);
            }
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
            index = UnityEngine.Random.Range(0, 2);
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

        // play spawning audio
        PlayAudio();

        // Spawn a zombie
        GameObject Zombie = Instantiate(zombArray[index], this.transform.position, Quaternion.identity);

        // Increment the total # of zombies in the scene
        ZombieManager.totalSpawnedZombies++;
        ZombieManager.totalZombiesAlive++;

        // Either Andrew or Tanner did this part
        zombieAIV1 zombieScript = Zombie.GetComponent<zombieAIV1>();

        if (zombieScript != null)
        {
            zombieScript.SetMinigameScript(miniGameScript.instance);
        }

        zombieManager.zombies.Add(Zombie);

        
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
        }
        else
        {
            playerNear = true;
        }
    }

    // Update the can spawn variable
    void CanSpawnUpdate()
    {
        if (ZombieManager.totalZombiesAlive >= maxAliveZombies)
        {
            canSpawn = false;
        }
        else if (ZombieManager.spawnMaxReached)
        {
            canSpawn = false;
        }
        else if (ZombieManager.totalZombiesAlive < maxAliveZombies)
        {
            canSpawn = true;

        }        
    }

    void PlayAudio()
    {
        spawnerSource.Play();
    }

    // shows us the spawn range in the editor
    private void OnDrawGizmos()
    {
        // Set the color with custom alpha.
        Gizmos.color = new Color(1f, 0f, 0f, alpha); // Red with custom alpha

        // Draw the sphere.
        Gizmos.DrawSphere(transform.position, spawnRange);

        // Draw wire sphere outline.
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }
}
