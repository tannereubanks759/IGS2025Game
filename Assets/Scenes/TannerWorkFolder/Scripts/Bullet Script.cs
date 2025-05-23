using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.VFX;

public class BulletScript : MonoBehaviour
{
    public float velocity = 100;
    private Rigidbody rb;
    private float lifeTimer = 8f; //Seconds
    private float nextTime;
    public ParticleSystem bulletImpact;
    public GameObject[] zombieImpacts;
    public GameObject zombieImpact;
    public AudioSource bulletImpactSound;
    public AudioClip headshotSound;
    public AudioClip bodyshotSound;
    public AudioClip normalImpactSound;
    public balloonMinigame balloonMinigameRef;
    public static int baseDamage = 1;
    private GunScript gun;
    private float bulletRadius; //goes by pixel
    public bool isMiniBullet;
    public AudioClip balloonPopSound;
    public AudioSource balloonSource;
    public LayerMask layers;
    
    void Start()
    {
        
        int bulletImpactRandom = Random.Range(0, zombieImpacts.Length);
        zombieImpact = zombieImpacts[bulletImpactRandom];
        if (!isMiniBullet)
        {
            gun = GameObject.FindAnyObjectByType<GunScript>();
        }
        if(!isMiniBullet && gun.bulletCount % 3 == 0 )
        {
            this.GetComponentInChildren<TrailRenderer>().enabled = true;
        }
        else
        {
            this.GetComponentInChildren<TrailRenderer>().enabled = true;
        }
        bulletRadius = GameObject.Find("RightSideRet").GetComponent<RectTransform>().anchoredPosition.x;
        //Debug.Log(bulletRadius);
        float randomx = Random.Range(-bulletRadius, bulletRadius);
        float randomy = Random.Range(-bulletRadius, bulletRadius);
        Vector3 ScreenPos = Input.mousePosition + new Vector3(randomx, randomy, 0);
        Ray Ray = Camera.main.ScreenPointToRay(ScreenPos);
        if(Physics.Raycast(Ray, out RaycastHit hit, layers))
        {
            this.transform.LookAt(hit.point);
        }
        balloonMinigameRef = GameObject.FindFirstObjectByType<balloonMinigame>();
        zombieImpact.SetActive(false);
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
        //Debug.Log(collision.gameObject.name + ", Game Active: " + balloonMinigameRef.isMiniActive);
        if (collision.gameObject.layer == 8)
        {
            float randomPitch = Random.Range(.8f, 1.3f);
            bulletImpactSound.pitch = randomPitch;
            bulletImpactSound.PlayOneShot(bodyshotSound, .2f);
            collision.gameObject.GetComponentInParent<zombieAIV1>().TakeDamage(baseDamage);
            zombieImpact.SetActive(true);
            //zombieImpact.Play();
            
        }
        else if (collision.gameObject.layer == 9) //hithead
        {
            float randomPitch = Random.Range(.8f, 1.3f);
            bulletImpactSound.pitch = randomPitch;
            bulletImpactSound.PlayOneShot(headshotSound, .2f);
            zombieImpact.SetActive(true);
            //zombieImpact.Play();
            //collision.gameObject.GetComponentInChildren<VisualEffect>().Play();
            collision.transform.parent.localScale = new Vector3(.01f, .01f, .01f);
            collision.gameObject.GetComponentInChildren<VisualEffect>().gameObject.transform.localScale = new Vector3(100f, 100f, 100f);
            collision.gameObject.GetComponentInParent<zombieAIV1>().TakeDamageOnHead(baseDamage + 2);
            
        }
        else if (collision.gameObject.layer == 16) //hit left arm
        {
            zombieAIV1 zomScript = collision.gameObject.GetComponentInParent<zombieAIV1>();
            float randomPitch = Random.Range(.8f, 1.3f);
            bulletImpactSound.pitch = randomPitch;
            bulletImpactSound.PlayOneShot(headshotSound, .2f);
            zombieImpact.SetActive(true);
            //zombieImpact.Play();
            //zomScript.leftArm.GetComponentInChildren<VisualEffect>().gameObject.SetActive(true);
            zomScript.leftArmExplode.SetActive(true);
            zomScript.leftArm.transform.localScale = new Vector3(.01f, .01f, .01f);
            zomScript.leftArm.GetComponentInChildren<VisualEffect>().gameObject.transform.localScale = new Vector3(100f, 100f, 100f);
            zomScript.TakeDamage(baseDamage);

        }
        else if (collision.gameObject.layer == 17) //hit right arm
        {
            zombieAIV1 zomScript = collision.gameObject.GetComponentInParent<zombieAIV1>();
            float randomPitch = Random.Range(.8f, 1.3f);
            bulletImpactSound.pitch = randomPitch;
            bulletImpactSound.PlayOneShot(headshotSound, .2f);
            zombieImpact.SetActive(true);
            //zombieImpact.Play();
            //zomScript.rightArm.GetComponentInChildren<VisualEffect>().gameObject.SetActive(true);
            zomScript.rightArmExplode.SetActive(true);
            zomScript.rightArm.transform.localScale = new Vector3(.01f, .01f, .01f);
            zomScript.rightArm.GetComponentInChildren<VisualEffect>().gameObject.transform.localScale = new Vector3(100f, 100f, 100f);
            zomScript.TakeDamage(baseDamage);

        }
        else if (collision.gameObject.layer == 15) //Hit C4
        {
            //Debug.Log("HitC4");
            zombieAIV1 zomScript = collision.gameObject.GetComponentInParent<zombieAIV1>();
            if (!zomScript.c4Active)
            {
                zomScript.c4Active = true;
                collision.gameObject.GetComponent<AudioSource>().Play();
                //collision.gameObject.GetComponent<Collider>().enabled = false;
                collision.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                float randomPitch = Random.Range(.8f, 1.3f);
                bulletImpactSound.pitch = randomPitch;
                bulletImpactSound.PlayOneShot(headshotSound, .5f);
                zomScript.TakeDamage(baseDamage);
                zombieImpact.SetActive(true);
                //zombieImpact.Play();
                zomScript.Explode();
                Destroy(this.gameObject);
            }
            else
            {
                zomScript.C4Death();
            }
            

        }
        /*else if(collision.gameObject.layer==13 && balloonMinigameRef.isMiniActive)
        {
            balloonMinigameRef.shotRightBalloon(collision);
            Destroy(collision.gameObject);
        }*/
        else if(collision.gameObject.layer==14 && balloonMinigameRef.isMiniActive)
        {
            balloonMinigameRef.shotRightBalloon(collision);
            collision.gameObject.SetActive(false);
            balloonSource.PlayOneShot(balloonPopSound, .2f);
        }
        else
        {
            float randomPitch = Random.Range(.8f, 1.3f);
            bulletImpactSound.pitch = randomPitch;
            //bulletImpactSound.PlayOneShot(normalImpactSound, .1f);
            bulletImpact.Play();
            rb.isKinematic = true;
        }
        nextTime += .3f;
        rb.isKinematic = true;
        this.GetComponentInChildren<MeshRenderer>().enabled = false;
        this.GetComponent<Collider>().enabled = false;
    }
}
