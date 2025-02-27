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
    public AudioSource bulletImpactSound;
    public AudioClip headshotSound;
    public AudioClip bodyshotSound;
    public AudioClip normalImpactSound;
    public balloonMinigame balloonMinigameRef;
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
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.layer == 8)
        {
            float randomPitch = Random.Range(.8f, 1.3f);
            bulletImpactSound.pitch = randomPitch;
            bulletImpactSound.PlayOneShot(bodyshotSound);
            collision.gameObject.GetComponentInParent<zombieAIV1>().TakeDamage(1);
            Debug.Log("Hit Zombie");
            zombieImpact.gameObject.SetActive(true);
            zombieImpact.Play();
            
        }
        else if (collision.gameObject.layer == 9)
        {
            float randomPitch = Random.Range(.8f, 1.3f);
            bulletImpactSound.pitch = randomPitch;
            bulletImpactSound.PlayOneShot(headshotSound);
            zombieImpact.gameObject.SetActive(true);
            zombieImpact.Play();
            collision.gameObject.GetComponentInChildren<VisualEffect>().Play();
            collision.transform.parent.localScale = new Vector3(.01f, .01f, .01f);
            collision.gameObject.GetComponentInChildren<VisualEffect>().gameObject.transform.localScale = new Vector3(100f, 100f, 100f);
            collision.gameObject.GetComponentInParent<zombieAIV1>().TakeDamage(3);
            Debug.Log("Hit Zombie head");
            
        }
        else if(collision.gameObject.layer==14 && balloonMinigameRef.isMiniActive)
        {
            //Debug.Log("HIT Gold BALL");
            balloonMinigameRef.shotRightBalloon();
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.layer==14 && balloonMinigameRef.isMiniActive)
        {
            //Debug.Log("HIT RED BALL");
            balloonMinigameRef.shotWrongBalloon();
            Destroy(collision.gameObject);
        }
        else
        {
            float randomPitch = Random.Range(.8f, 1.3f);
            bulletImpactSound.pitch = randomPitch;
            bulletImpactSound.PlayOneShot(normalImpactSound, .5f);
            Debug.Log("bullet collided");
            bulletImpact.Play();
            rb.isKinematic = true;
        }
        nextTime += .3f;
        rb.isKinematic = true;
    }
}
