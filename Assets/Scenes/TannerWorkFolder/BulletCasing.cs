using UnityEngine;

public class BulletCasing : MonoBehaviour
{
    public float velocity = .01f;
    private Rigidbody rb;
    private float nextTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextTime = Time.time + 3;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(this.transform.forward * velocity, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextTime)
        {
            Destroy(this.gameObject);
        }
    }
}
