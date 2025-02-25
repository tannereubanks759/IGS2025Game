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

    public int maxRunningAnimCount;

    // ZombieManager refs
    private GameObject zombieManagerOBJ;
    private ZombieManager zombieManager;

    // allows for only 1 pass of the death logic
    private bool deathGateKeeper;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        miniGameS = miniGameScript.instance;

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

        // Set this to false so that the death logic will pass through
        // the first time only
        deathGateKeeper = false;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerDetected();
        MoveAI();
        CanAttack();
        // IsDead();
        OnFire();
        
    }

    void OnFire()
    {
        // If the zombie is on fire (set by the flamethrowerDamage Script)
        if (onFire)
        {
            // Increment the timer
            fireTimer += Time.deltaTime;

            // The zombie has been on fire for 3 seconds
            if (fireTimer > 3)
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
                onFire = false;
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
        if (animator.GetBool("playerDetected") && animator.GetBool("isStanding") && !animator.GetBool("canAttack") && (animator.GetInteger("health") >= 1))
        {
            // set the agent's destination to be the player's position
            agent.destination = player.transform.position;
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
            // If a headshot
            if (i == 3)
            {
                
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
       
    }

    // Checks if the AI is dead
    public void IsDead()
    {
        // Decrement the total # of zombies
        zombieManager.totalZombiesAlive--;

        // Anim plays multiple times, not sure if the agent.isStopped line is even working
        agent.isStopped = true;

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
        Destroy(this.gameObject);
    }
    public void SetMinigameScript(miniGameScript script)
    {
        miniGameS = script;
    }
}
