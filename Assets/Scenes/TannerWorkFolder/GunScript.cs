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
        //anim control for walking and running
        if (playerScript.isSprinting)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", true);
        }
        else if (playerScript.isWalking)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
        }











        //anim for aiming
        if (Input.GetKey(AimKey))
        {
            anim.SetBool("isAiming", true);
        }
        else
        {
            anim.SetBool("isAiming", false);
        }




        //anim for shooting
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







        //anim for reloading
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
