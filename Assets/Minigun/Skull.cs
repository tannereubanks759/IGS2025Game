using UnityEngine;

public class Skull : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float nextTime;
    public float Lifespan;

    void Start()
    {
        nextTime = Lifespan + Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(nextTime < Time.deltaTime)
        {
            Destroy(this.gameObject.GetComponentInParent<Animator>().gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == 11)
        {
            Camera.main.GetComponent<GunHandler>().ActivateBuff();
            Destroy(this.gameObject.GetComponentInParent<Animator>().gameObject);
        }
    }
}
