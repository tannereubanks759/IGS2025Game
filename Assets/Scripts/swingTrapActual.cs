using System.Runtime.CompilerServices;
using UnityEngine;

public class swingTrapActual : MonoBehaviour
{
    public swingTrap swingTrapRef;
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
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponentInParent<zombieAIV1>().TakeDamage(4);
            
        }

    }
    private void OnTriggerEnter(Collider other)
    {
      
    }
    public void callResetFunctin()
    {
        swingTrapRef.resetAnimationTrigger();
    }
}
