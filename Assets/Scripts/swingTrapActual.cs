using System.Runtime.CompilerServices;
using UnityEngine;

public class swingTrapActual : MonoBehaviour
{
    public swingTrap swingTrapRef;
    public PlayerHealthManager playerHealthManagerRef;
    public float trapForce = 14f;
    public Transform centerOfShip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.layer == 11)
        {
            playerHealthManagerRef.pause.Die();
        }
        else if (collision.gameObject.layer == 8)
        {
            Vector3 directionToPush = (collision.transform.position - centerOfShip.position).normalized;

            Vector3 forceDirection = Quaternion.Euler(0, 90, 0) * directionToPush;
            collision.gameObject.GetComponentInParent<ragdollScript>().startRagdoll(forceDirection, trapForce);
            collision.gameObject.GetComponentInParent<zombieAIV1>().TakeDamage(10);
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
      
    }
    public void callResetFunctin()
    {
        swingTrapRef.trapAnim.SetBool("isOn", false);
        swingTrapRef.paid = false;
        
    }
}
