using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float destroyTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("DestroyGrenade", destroyTime);
    }

    public void DestroyGrenade()
    {
        Destroy(this.gameObject);
    }
}
