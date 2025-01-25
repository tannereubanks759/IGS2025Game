using UnityEngine;
using UnityEngine.AI;

public class zombieAIV1 : MonoBehaviour
{
    // The player
    [SerializeField] GameObject player = null;

    // The navmesh agent controlling the zombie ai
    NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the navmesh agent from the gameObject
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // The zombie has a valid player to target
        if (player != null)
        {
            // set the agent's destination to be the player's position
            agent.destination = player.transform.position;
        }
    }
}
