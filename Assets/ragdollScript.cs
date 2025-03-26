using UnityEngine;

public class ragdollScript : MonoBehaviour
{
    private Animator zombieAnimator;
    private Rigidbody[] ragdollRigids;
    //private Collider[] ragdollColliders;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zombieAnimator = GetComponent<Animator>();    
        ragdollRigids = GetComponentsInChildren<Rigidbody>();
        //ragdollColliders = GetComponentsInChildren<Collider>();
        foreach (Rigidbody rb in ragdollRigids)
        {
            rb.isKinematic = true;
        }
    }
    public void startRagdoll(Vector3 forceDirection, float forceStrength)
    {
        zombieAnimator.enabled = false;
        foreach (Rigidbody rb in ragdollRigids) 
        { 
            rb.isKinematic = false;
            rb.AddForce(forceDirection * forceStrength, ForceMode.Impulse);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
