using UnityEngine;
using UnityEngine.VFX;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;

public class GrenadeScript : MonoBehaviour
{

    public float throwForce;
    public float explodeTime;
    public GameObject ExplodePref;
    private AudioSource source;
    public AudioClip rock;
    public GameObject GrenadeBody;
    public bool isActive;

    public float forceStrength = 10f;
    

    

    void Start()
    {
        isActive = false;
        source = this.GetComponent<AudioSource>();
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.AddForce(this.transform.forward * throwForce, ForceMode.Impulse);
    }

    void Explode()
    {
        Destroy(GrenadeBody);
        //this.GetComponent<Collider>().enabled = false;
        Vector3 position = this.transform.position;
        zombieAIV1[] zombies = GameObject.FindObjectsByType<zombieAIV1>(FindObjectsSortMode.None);
        float distancefromplayer = Vector3.Distance(position, GameObject.FindAnyObjectByType<FirstPersonController>().transform.position);
        
        
        
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
            else if (distancefromplayer < 2)
            {
                manager.TakeDamage();
                manager.TakeDamage();
                manager.TakeDamage();
            }
            else if (distancefromplayer < 3)
            {
                manager.TakeDamage();
                manager.TakeDamage();
            }
            else
            {
                manager.TakeDamage();
            }

        } //Player Damage
        
        
        for (int i = 0; i < zombies.Length; i++)
        {
            if (Vector3.Distance(position, zombies[i].gameObject.transform.position) < 4)
            {
                zombieAIV1 zombieAI = zombies[i];
                zombieAI.bloodGameObject.SetActive(true);
                if (zombieAI.isDead != true)
                {

                    zombieAI.GetComponent<NavMeshAgent>().enabled = false;
                    if(zombieAI.ExplodePref != null)
                    {
                        zombieAI.C4Death(zombies);
                    }
                    else
                    {
                        // calculates the direction away from the middle of the trap and the position of zombie
                        Vector3 directionToPush = (zombieAI.transform.position - this.transform.position).normalized;


                        zombieAI.gameObject.GetComponent<ragdollScript>().startRagdoll(directionToPush, forceStrength);
                        zombieAI.IsDeadRagDoll();
                        zombieAI.Death();
                    }
                    
                }
            }
        } //Zombie killing
        
        
        
        Instantiate(ExplodePref, this.transform.position, Quaternion.identity);
        this.GetComponent<VisualEffect>().Stop();
        Invoke("Delete", 2f);
        //Destroy(this.gameObject);
    }

    IEnumerator deathFunctions(zombieAIV1 zombieAI)
    {
        yield return new WaitForSeconds(2f);
        if (zombieAI != null)
        {
            
            zombieAI.Death();
            //Destroy(zombieAI.gameObject);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isActive != true)
        {
            isActive = true;
            Explode();
        }
        
    }

    private void Delete()
    {
        Destroy(this.gameObject);
    }
}
