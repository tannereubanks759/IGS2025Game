using UnityEngine;

public class Skull : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float nextTime;
    public float Lifespan;
    private float destroyTime = -1f;
    void Start()
    {
        nextTime = Time.time+Lifespan;
    }

    // Update is called once per frame
    void Update()
    {
        if(nextTime < Time.time)
        {
            if (destroyTime == -1f) {
                destroyTime = Time.time + 1f;
                this.gameObject.GetComponentInParent<Animator>().SetTrigger("DESTROY");
            }
            if(Time.time > destroyTime)
            {
                delete();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == 11)
        {
            Camera.main.GetComponent<GunHandler>().ActivateBuff();
            zombieAIV1.buffInScene = false;
            Destroy(this.gameObject.GetComponentInParent<Animator>().gameObject);
        }
    }
    private void delete()
    {
        Destroy(this.gameObject.GetComponentInParent<Animator>().gameObject);
    }
}
