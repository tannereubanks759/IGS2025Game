using UnityEngine;
using UnityEngine.VFX;

public class GunScript : MonoBehaviour
{
    
    public GameObject bulletSpawnPoint;
    public GameObject bullet;
    public KeyCode ShootKey;
    public KeyCode AimKey;
    public KeyCode ReloadKey;
    public VisualEffect muzzleFlash;
    public float fireRate;
    public float magazineSize;
    public float bulletCount;
    public Animator anim; //booleans: isWalking isRunning isAiming isFiring isReloading
    private float nextFire;
    public FirstPersonController playerScript;

    void Start()
    {
        bulletCount = magazineSize;
        nextFire = Time.time;
    }
    void Update()
    {
        

        if (Input.GetKey(AimKey))
        {
            anim.SetBool("isAiming", true);
        }
        else
        {
            anim.SetBool("isAiming", false);
        }
        if (Input.GetKey(ShootKey))
        {
            if(nextFire < Time.time && bulletCount > 0f) //has ammo
            {
                Fire();
            }
            else if(nextFire < Time.time && bulletCount <= 0f) //does not have ammo
            {
                //play tick sound here
            }
            if(bulletCount > 0f)
            {
                anim.SetBool("isFiring", true);
            }
        }
        else
        {
            anim.SetBool("isFiring", false);
        }

        if (Input.GetKeyDown(ReloadKey))
        {
            anim.SetTrigger("isReloading");
            //Reload();
        }
    }
    public void Fire()
    {
        Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        muzzleFlash.Play();
        nextFire = Time.time + fireRate;
        bulletCount -= 1f;
        Debug.Log("Fire");
    }

    public void Reload()
    {
        bulletCount = magazineSize;
    }
}
