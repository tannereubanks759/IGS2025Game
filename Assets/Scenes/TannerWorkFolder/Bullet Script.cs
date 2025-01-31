using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float velocity = 100;
    private Rigidbody rb;
    private float lifeTimer = 1f; //Seconds
    private float nextTime;
    private ParticleSystem bulletImpact;
    miniGameScript miniScript;
    void Start()
    {
        bulletImpact = this.GetComponent<ParticleSystem>();
        nextTime = Time.time + lifeTimer;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(this.transform.forward * velocity, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextTime)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponentInParent<zombieAIV1>().TakeDamage(1);
            nextTime += .3f;
            Debug.Log("Hit Zombie");
            bulletImpact.Play();
            rb.isKinematic = true;
        }
        else if (collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponentInParent<zombieAIV1>().TakeDamage(3);
            nextTime += .3f;
            Debug.Log("Hit Zombie head");
            bulletImpact.Play();
            rb.isKinematic = true;
        }
        else
        {
            nextTime += .3f;
            Debug.Log("bullet collided");
            bulletImpact.Play();
            rb.isKinematic = true;
        }
        
        
    }
}
