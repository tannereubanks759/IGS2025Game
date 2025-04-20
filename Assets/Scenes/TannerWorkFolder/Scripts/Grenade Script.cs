using UnityEngine;

public class GrenadeScript : MonoBehaviour
{

    public float throwForce;
    public float explodeTime;
    public GameObject ExplodePref;
    private AudioSource source;
    public AudioClip rock;
    void Start()
    {
        source = this.GetComponent<AudioSource>();
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.AddForce(this.transform.forward * throwForce, ForceMode.Impulse);
        Invoke("Explode", explodeTime);
    }

    void Explode()
    {
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

        }
        for (int i = 0; i < zombies.Length; i++)
        {
            if (Vector3.Distance(position, zombies[i].gameObject.transform.position) < 4)
            {
                zombies[i].TakeDamage(10);
            }
        }
        Instantiate(ExplodePref, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        source.PlayOneShot(rock, .5f);
    }
}
