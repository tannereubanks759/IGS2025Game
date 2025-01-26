using UnityEngine;
using UnityEngine.AI;

public class zombieAIV1 : MonoBehaviour
{
    // The player
    [SerializeField] GameObject player = null;

    // The navmesh agent controlling the zombie ai
    NavMeshAgent agent;

    // The animator attached to the zombie
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the navmesh agent from the gameObject
        agent = GetComponent<NavMeshAgent>();

        // Get the animator
        animator = GetComponent<Animator>();

        // Get the player
        
    }

    // Update is called once per frame
    void Update()
    {
        //// The zombie has a valid player to target
        //if (player != null)
        //{
        //    // set the agent's destination to be the player's position
        //    agent.destination = player.transform.position;
        //}
    }

    // Set the is standing bool to true
    public void IsStanding()
    {
        animator.SetBool("isStanding", true);
    }
}
