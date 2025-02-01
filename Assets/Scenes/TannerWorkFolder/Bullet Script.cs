using UnityEngine;
using UnityEngine.VFX;

public class BulletScript : MonoBehaviour
{
    public float velocity = 100;
    private Rigidbody rb;
    private float lifeTimer = 1f; //Seconds
    private float nextTime;
    public ParticleSystem bulletImpact;
    public VisualEffect zombieImpact;

    miniGameScript miniScript;
    void Start()
    {
        zombieImpact.gameObject.SetActive(false);
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
            
            Debug.Log("Hit Zombie");
            zombieImpact.gameObject.SetActive(true);
            zombieImpact.Play();
            
        }
        else if (collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponentInParent<zombieAIV1>().TakeDamage(3);
            Debug.Log("Hit Zombie head");
            zombieImpact.gameObject.SetActive(true);
            zombieImpact.Play();
        }
        else
        {
            Debug.Log("bullet collided");
            bulletImpact.Play();
            rb.isKinematic = true;
        }
        nextTime += .3f;
        rb.isKinematic = true;
    }
}
