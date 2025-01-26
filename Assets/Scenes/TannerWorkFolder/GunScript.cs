using UnityEngine;
using UnityEngine.VFX;

public class GunScript : MonoBehaviour
{
    
    public GameObject bulletSpawnPoint;
    public GameObject bullet;
    public KeyCode ShootKey;
    public KeyCode AimKey;
    public VisualEffect muzzleFlash;
    public float fireRate;
    private float nextFire;
    void Start()
    {
        nextFire = Time.time;
    }
    void Update()
    {
        if (Input.GetKey(ShootKey) && nextFire < Time.time)
        {
            Fire();
        }
    }
    private void Fire()
    {
        Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        muzzleFlash.Play();
        nextFire = Time.time + fireRate;
        Debug.Log("Fire");
    }
}
