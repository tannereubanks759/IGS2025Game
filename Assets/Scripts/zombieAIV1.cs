using UnityEngine;
using UnityEngine.AI;

public class zombieAIV1 : MonoBehaviour
{
    // The player
    GameObject player = null;

    // The navmesh agent controlling the zombie ai
    NavMeshAgent agent;

    // The animator attached to the zombie
    Animator animator;

    // The number that controls the melee range of the ai
    float attackRange = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the navmesh agent from the gameObject
        agent = GetComponent<NavMeshAgent>();

        // Get the animator
        animator = GetComponent<Animator>();

        // Get the player
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDetected();
        MoveAI();
        CanAttack();
    }

    // Set the is standing bool to true
    public void IsStanding()
    {
        animator.SetBool("isStanding", true);
    }

    // Sets the animator bool for player detection
    void PlayerDetected()
    {
        if (player != null)
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
        if (animator.GetBool("playerDetected") && animator.GetBool("isStanding") && !animator.GetBool("canAttack"))
        {
            // set the agent's destination to be the player's position
            agent.destination = player.transform.position;
        }
    }

    // Checks if the AI can attack
    void CanAttack()
    {
        
        if ((player.transform.position - this.transform.position).magnitude < attackRange)
        {
            agent.isStopped = true;
            animator.SetBool("canAttack", true);
        }
        else
        {
            agent.isStopped = false;
            animator.SetBool("canAttack", false);
        }
    }
}
