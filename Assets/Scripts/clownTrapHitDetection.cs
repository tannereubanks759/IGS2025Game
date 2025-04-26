using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Processors;

public class clownTrapHitDetection : MonoBehaviour
{
    public PlayerHealthManager playerHealthManagerRef;
    public ClownRideMovement clownRideMovementRef;
    public float forceStrength = 10f;
    public Transform centerOfTrap;
    public ZombieManager zombieManagerRef;
    private void Start()
    {
        zombieManagerRef = FindAnyObjectByType<ZombieManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if(clownRideMovementRef.isActive)
        {
            if (collision.gameObject.layer == 8)
            {
               
                zombieAIV1 zombieAI = collision.gameObject.GetComponentInParent<zombieAIV1>();
                zombieAI.bloodGameObject.SetActive(true);
                if(zombieAI.isDead!=true) 
                {
                    // calculates the direction away from the middle of the trap and the position of zombie
                    Vector3 directionToPush = (collision.transform.position - centerOfTrap.position).normalized;
                    // makes the direction to the left of the trap since blades are spinning that way
                    Vector3 forceDirection = Quaternion.Euler(0, 90, 0) * directionToPush;
                    //zombie takes damage
                    zombieAI.TakeDamage(10);
                    zombieAI.isDead = true;
                    //ragdoll is started
                    collision.gameObject.GetComponentInParent<ragdollScript>().startRagdoll(forceDirection, forceStrength);



                    StartCoroutine(deathFunctions(zombieAI));
                }
               

            }
            //IF PLAYER GETS HIT
            if (collision.gameObject.layer == 11)
            {
                playerHealthManagerRef.TakeDamage();
            }
        }
       
    }
    
    IEnumerator deathFunctions(zombieAIV1 zombieAI) 
    {
        yield return new WaitForSeconds(2f);
        if(zombieAI!=null)
        {
            zombieAI.IsDead();

            zombieAI.Death();
            Destroy(zombieAI.gameObject);

            /*zombieManagerRef.totalZombiesAlive--;
            zombieManagerRef.totalZombiesKilled++;*/

            
        }
       
        //zombieAI.IsDead();
       
        //zombieAI.Death();
    }
}
