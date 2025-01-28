using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float velocity = 100;
    private Rigidbody rb;
    private float lifeTimer = 1f; //Seconds
    private float nextTime;
    private ParticleSystem bulletImpact;
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
        nextTime += .3f;
        Debug.Log("bullet collided");
        //Destroy(this.gameObject);
        bulletImpact.Play();
        rb.isKinematic = true;
        
    }
}
