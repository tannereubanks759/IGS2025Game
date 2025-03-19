using UnityEngine;
using UnityEngine.AI;
using System.Collections;

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
    [SerializeField] private Collider leftHand;

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
    
    public  bool c4Active;
    public GameObject ExplodePref;
    public GameObject rightArm;
    public GameObject leftArm;
    public ClownRideMovement clownRideMovementRef;
    public GameObject clowntrap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        miniGameS = miniGameScript.instance;
        c4Active = false;
        clownRideMovementRef =  FindAnyObjectByType<ClownRideMovement>();
        clowntrap = GameObject.FindWithTag("Middle Clown Point");
        // Get the navmesh agent from the gameObject
        agent = GetComponent<NavMeshAgent>();

        // Get the animator
        animator = GetComponent<Animator>();

        // Get the player
        player = GameObject.FindWithTag("Player");

        // turn attack collider off by deafault
        rightHand.enabled = false;
        leftHand.enabled = false;

        int randomRun = Random.Range(0, maxRunningAnimCount);
        animator.SetInteger("randRun", randomRun);

        // Find and get the zombie manager script reference
        zombieManagerOBJ = GameObject.FindGameObjectWithTag("ZombieManager");
        zombieManager = zombieManagerOBJ.GetComponent<ZombieManager>();

        onFire = false;
        onFirePS.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.enabled == true)
        {
            PlayerDetected();
            MoveAI();
            CanAttack();
            OnFire();
            LastAlive();
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
        if(isDead!=true)
        {
            animator.SetInteger("health", animator.GetInteger("health") - i);
            animator.SetTrigger("takeDamage");
            
        }
       
    }
    public void TakeDamageOnHead(int i)
    {
        // Set the health by getting the health and subtracting 1
        if (isDead != true)
        {
            animator.SetInteger("health", animator.GetInteger("health") - i);
            animator.SetTrigger("takeDamage");
            
            // and the headshot minigame is active
            if (miniGameS.headShotQuest == true)
            {

                miniGameS.currentHeadShots += 1;
                Debug.Log("Headshot tracked");
                isDead = true;
                miniGameS.printHs();
            }
            
        }
    }

    // Checks if the AI is dead
    public void IsDead()
    {
        // Decrement the total # of zombies
        zombieManager.totalZombiesAlive--;

        zombieManager.totalZombiesKilled++;

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
        leftHand.enabled = true;
    }

    // De-activates the attack colliders
    public void DeactivateColliders()
    {
        rightHand.enabled = false;
        leftHand.enabled = false;
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
        if (zombieManager.totalZombiesAlive == 1 && zombieManager.spawnMaxReached)
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
        animator.SetBool("c4Active", true);
        agent.speed = agent.speed * 7;
        Invoke("C4Death", 3);
    }

    //Called from Explode function
    //Purpose: Play Explode effect and remove zombie from scene.
    public void C4Death()
    {
        Instantiate(ExplodePref, this.transform.position + new Vector3(0, 1, 0), this.transform.rotation);
        // Decrement the total # of zombies
        zombieManager.totalZombiesAlive--;
        zombieManager.totalZombiesKilled++;
        Death();
       
    }
}
