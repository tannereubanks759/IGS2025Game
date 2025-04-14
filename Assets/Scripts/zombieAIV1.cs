using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.VFX;
using Unity.VisualScripting;

public class zombieAIV1 : MonoBehaviour
{
    // The player
    GameObject player = null;

    // The navmesh agent controlling the zombie ai
    NavMeshAgent agent;

    // The animator attached to the zombie
    Animator animator;

    // The number that controls the melee range of the ai
    float attackRange = 2f;

    // Colliders to handle the ai attacking the player
    [SerializeField] private Collider rightHand;

    public bool isDead = false;
    private miniGameScript miniGameS;
    public GameObject objWscript;

    // On fire variables
    public bool onFire;
    private float fireTimer;
    [SerializeField] float fireTickTime = 1f;
    [SerializeField] ParticleSystem onFirePS;

    public int maxRunningAnimCount;

    // ZombieManager refs
    private GameObject zombieManagerOBJ;
    private ZombieManager zombieManager;

    // values for having the zombie be the last alive
    private float lastAliveTimer;
    private bool isLastAlive = false;

    // the spawn effect
    [SerializeField] private VisualEffect spawnEffect;

    // audio files/references
    private AudioSource audioSource;
    [SerializeField] private AudioClip footStep;
    [SerializeField] private AudioClip hurt;
    [SerializeField] private AudioClip deathSound;

    // variable to track players distance form the zombies
    public float playerDist;

    public bool c4Active;
    public GameObject ExplodePref;
    public GameObject rightArm;
    public GameObject leftArm;
    public ClownRideMovement clownRideMovementRef;
    public GameObject clowntrap;
    public GameObject bloodParticleObject;
    public GameObject testBloodSpray;
    public GameObject head;

    public bool hitByTrap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitByTrap = false;
        bloodParticleObject.SetActive(false);
        miniGameS = FindAnyObjectByType<miniGameScript>();
        c4Active = false;
        clownRideMovementRef = FindAnyObjectByType<ClownRideMovement>();
        clowntrap = GameObject.FindWithTag("Middle Clown Point");
        // Get the navmesh agent from the gameObject
        agent = GetComponent<NavMeshAgent>();

        // Get the animator
        animator = GetComponent<Animator>();

        // Get the player
        player = GameObject.FindWithTag("Player");

        // turn attack collider off by deafault
        rightHand.enabled = false;

        int randomRun = Random.Range(0, maxRunningAnimCount);
        animator.SetInteger("randRun", randomRun);

        // Find and get the zombie manager script reference
        zombieManagerOBJ = GameObject.FindGameObjectWithTag("ZombieManager");
        zombieManager = zombieManagerOBJ.GetComponent<ZombieManager>();

        onFire = false;
        onFirePS.Stop();

        spawnEffect = GetComponentInChildren<VisualEffect>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled == true)
        {
            PlayerDetected();
            MoveAI();
            CanAttack();
            OnFire();
            //LastAlive();

            if (ExplodePref != null)
            {
                PlayerDistance();
                Respawn();
            }
        }
    }

    void OnFire()
    {
        // If the zombie is on fire (set by the flamethrowerDamage Script)
        if (onFire)
        {
            // Plays the onFirePS
            if (!onFirePS.isPlaying)
            {
                onFirePS.Play();
            }

            // Increment the timer
            fireTimer += Time.deltaTime;

            // The zombie has been on fire for 3 seconds
            if (fireTimer > fireTickTime)
            {
                // The zombie takes damage
                TakeDamage(1);

                // The timer is reset
                fireTimer = 0;

                /* 
                 * The onFire variable is reset so that zombies 
                 * do not just burn out from 1 hit. More particle
                 * collision will just reset the on fire variable
                 * and keep the timer/damage going at the 1 damage
                 * for every 3 seconds on fire rate
                 */
                // onFire = false;
            }
        }
    }

    // Set the is standing bool to true
    public void IsStanding()
    {
        StopParticles();

        animator.SetBool("isStanding", true);
    }

    // Sets the animator bool for player detection
    void PlayerDetected()
    {
        if (player != null && animator.GetBool("isStanding") == true)
        {
            // Set the animator bool to true so that the run anim plays
            animator.SetBool("playerDetected", true);
        }

        else
        {
            // Set the animator bool to false (should only be on player death)
            animator.SetBool("playerDetected", false);
        }
    }

    // Handles AI movement
    void MoveAI()
    {
        // if the trap isn't active, do regular pathfinding
        if (animator.GetBool("playerDetected") && animator.GetBool("isStanding") && !animator.GetBool("canAttack") && (animator.GetInteger("health") >= 1) && !clownRideMovementRef.isActive)
        {
            // set the agent's destination to be the player's position
            agent.destination = player.transform.position;
        }
        else if (animator.GetBool("playerDetected") && animator.GetBool("isStanding") && !animator.GetBool("canAttack") && (animator.GetInteger("health") >= 1) && clownRideMovementRef.isActive)
        {
            agent.destination = clowntrap.transform.position;
        }
        else
        {
            agent.isStopped = true;
        }
    }

    void PlayerDistance()
    {
        playerDist = Vector3.Distance(player.transform.position, this.transform.position);
    }

    void Respawn()
    {
        if (playerDist > 35)
        {
            ZombieManager.spawnMaxReached = false;

            //Debug.Log(ZombieManager.totalSpawnedZombies);
            ZombieManager.totalSpawnedZombies--;
            //Debug.Log(ZombieManager.totalSpawnedZombies);

            //Debug.Log(ZombieManager.totalZombiesAlive);
            ZombieManager.totalZombiesAlive--;
            //Debug.Log(ZombieManager.totalZombiesAlive);

            //Debug.Log(ZombieManager.totalZombiesKilled);
            ZombieManager.totalZombiesKilled--;
            //Debug.Log(ZombieManager.totalZombiesKilled);

            TakeDamage(10);

        }
    }

    // Checks if the AI can attack
    void CanAttack()
    {
        // There are times when the distance doesnt seem to line up (ie ().mag = 2f, which is greater than 1.5f)
        //Debug.Log((player.transform.position - this.transform.position).magnitude);

        // Set the canAttack bool in the animator if the player is in attack range
        if (Vector3.Distance(this.transform.position, player.transform.position) < attackRange)
        {
            // Stop the agent then attack
            animator.SetBool("canAttack", true);
            agent.isStopped = true;

        }
        else
        {
            // Activate the agent again then allow the player to move
            agent.isStopped = false;
            animator.SetBool("canAttack", false);
        }
    }

    // Public function to call when the zombie takes damage
    public void TakeDamage(int i)
    {
        // Set the health by getting the health and subtracting 1
        if (isDead != true && c4Active == false)
        {
            animator.SetInteger("health", animator.GetInteger("health") - i);
            animator.SetTrigger("takeDamage");

            // play hurt audio
            PlayHurt();

        }

    }
    public void trapBlood()
    {
        bloodParticleObject.SetActive(true);
    }
    public void TakeDamageOnHead(int i)
    {
        // Set the health by getting the health and subtracting 1
        if (isDead != true)
        {
            animator.SetInteger("health", animator.GetInteger("health") - i);
            animator.SetTrigger("takeDamage");
            GameObject brainChunks = Instantiate(testBloodSpray, head.transform.position, Quaternion.identity);
            Destroy(brainChunks, 5f);
            // and the headshot minigame is active
            if (miniGameS.headShotQuest == true)
            {

                miniGameS.currentHeadShots += 1;
                Debug.Log("Headshot tracked");
                isDead = true;
                miniGameS.printHs();
            }

            PlayHurt();

        }
    }

    // Checks if the AI is dead
    public void IsDead()
    {
        // Decrement the total # of zombies
        ZombieManager.totalZombiesAlive--;

        ZombieManager.totalZombiesKilled++;

        // Anim plays multiple times, not sure if the agent.isStopped line is even working
        
            agent.enabled = false;
        
        

        // Get the colliders on the gameobject
        Collider[] colliders = this.GetComponentsInChildren<Collider>();

        // Disable colliders
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }

    }

    // Activates the attack colliders
    public void ActivateColliders()
    {
        rightHand.enabled = true;
    }

    // De-activates the attack colliders
    public void DeactivateColliders()
    {
        rightHand.enabled = false;
    }


    // "Despawns" the bodies of dead zombies (add a fade out later)
    public void Death()
    {
        // stops the onFirePS if the zombie is on fire at the time of death
        if (onFire)
        {
            onFirePS.Stop();
        }

        Destroy(this.gameObject);
    }
    public void SetMinigameScript(miniGameScript script)
    {
        miniGameS = script;
    }

    void LastAlive()
    {
        if (ZombieManager.totalZombiesAlive == 1 && ZombieManager.spawnMaxReached)
        {
            isLastAlive = true;
        }

        if (isLastAlive)
        {
            lastAliveTimer += Time.deltaTime;

            if (lastAliveTimer > 45)
            {
                TakeDamage(10);
            }
        }
    }

    //Whenever the c4 on chest gets hit by a bullet, this gets called from the bullet script
    //Purpose: Make cop zombie start running and after a few seconds, it will explode.
    public void Explode()
    {
        c4Active = true;
        animator.SetBool("c4Active", true);
        agent.speed = agent.speed * 7;
        Invoke("C4Death", 3);
    }

    //Called from Explode function
    //Purpose: Play Explode effect and remove zombie from scene.
    public void C4Death()
    {
        Vector3 position = this.transform.position;
        zombieAIV1[] zombies = GameObject.FindObjectsByType<zombieAIV1>(FindObjectsSortMode.None);
        float distancefromplayer = Vector3.Distance(position, player.transform.position);
        if (distancefromplayer < 4)
        {
            PlayerHealthManager manager = GameObject.FindAnyObjectByType<PlayerHealthManager>();
            if (distancefromplayer < 1)
            {
                manager.TakeDamage();
                manager.TakeDamage();
                manager.TakeDamage();
                manager.TakeDamage();
                
            }
            else if(distancefromplayer < 2)
            {
                manager.TakeDamage();
                manager.TakeDamage();
                manager.TakeDamage();
            }
            else if(distancefromplayer < 3)
            {
                manager.TakeDamage();
                manager.TakeDamage();
            }
            else
            {
                manager.TakeDamage();
            }
            
        }
        for (int i = 0; i < zombies.Length; i++)
        {
            if (Vector3.Distance(position, zombies[i].gameObject.transform.position) < 4)
            {
                zombies[i].TakeDamage(10);
            }
        }

        Instantiate(ExplodePref, this.transform.position + new Vector3(0, 1, 0), this.transform.rotation);
        
        // Decrement the total # of zombies
        ZombieManager.totalZombiesAlive--;
        ZombieManager.totalZombiesKilled++;


        Death();
    }
    // Play the particle system
    void PlayParticles()
    {
        spawnEffect.Play();

    }

    void StopParticles()
    {
        spawnEffect.pause = true;
    }

    void PlayFootstep()
    {
        float random = Random.Range(.8f, 1.2f);
        audioSource.PlayOneShot(footStep, random);
    }

    void PlayHurt()
    {
        audioSource.PlayOneShot(hurt);
    }

    void PlayDeath()
    {
        audioSource.PlayOneShot(deathSound);
    }

    
}
