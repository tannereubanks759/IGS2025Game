
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;
using TMPro;
public class GunScript : MonoBehaviour
{
    
    public GameObject bulletSpawnPoint;
    public GameObject bullet;
    public KeyCode ShootKey;
    public KeyCode AimKey;
    public KeyCode ReloadKey;
    public VisualEffect muzzleFlash;
    //public float fireRate;
    public float magazineSize;
    public float bulletCount;
    private float totalAmmo;
    public Animator anim; //booleans: isWalking isRunning isAiming isFiring isReloading
    private float nextFire;
    public FirstPersonController playerScript;
    
    public float velocityThresh;
    public TextMeshProUGUI BulletText;

    //sounds
    public AudioSource gunSound;
    public AudioClip ammoEmptySound;
    public AudioClip unload;
    public AudioClip load;
    public AudioClip gunShot;

    void Start()
    {
        bulletCount = magazineSize;
        nextFire = Time.time;
        totalAmmo = 400;
        BulletText.text = bulletCount.ToString() + "/" + totalAmmo.ToString();
    }
    void Update()
    {
        //anim control for walking and running
        if (playerScript.isSprinting && playerScript.rb.linearVelocity.magnitude > velocityThresh)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", true);
        }
        else if ((Mathf.Abs(Input.GetAxis("Horizontal")) > .1f || Mathf.Abs(Input.GetAxis("Vertical")) > .1f) && playerScript.rb.linearVelocity.magnitude > velocityThresh)
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
        if (playerScript.cameraCanMove && Input.GetKey(ShootKey))
        {
            if(bulletCount > 0f)
            {
                anim.SetBool("isFiring", true);
            }
            else
            {
                anim.SetBool("isFiring", false);
                if (Time.time > nextFire)
                {
                    gunSound.PlayOneShot(ammoEmptySound, .5f);
                    nextFire = Time.time + 1f;

                }
            }
            
            
        }
        else
        {
            anim.SetBool("isFiring", false);
        }

        if (Input.GetKeyDown(ShootKey) && bulletCount <= 0f)
        {
            gunSound.PlayOneShot(ammoEmptySound, .5f);
        }


        //anim for reloading
        if (((Input.GetKeyDown(ReloadKey) && bulletCount != 30f) || (Input.GetKey(ShootKey) && bulletCount <= 0f)) && anim.GetBool("isReloading") == false)
        {
            anim.SetBool("isReloading", true);
            //Reload();
        }
    }
    public void Fire()
    {
        Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        muzzleFlash.Play();
        gunSound.pitch = Random.Range(.5f, 1.5f);
        gunSound.PlayOneShot(gunShot, .2f);
        if(bulletCount > 0)
        {
            bulletCount -= 1f;
        }
        BulletText.text = bulletCount.ToString() + "/" + totalAmmo.ToString();
        Debug.Log("Fire");
    }

    public void Reload()
    {
        gunSound.pitch = 1f;
        if(totalAmmo >= 30)
        {
            totalAmmo -= magazineSize - bulletCount;
            bulletCount = magazineSize;
        }
        else if(totalAmmo < 30 && totalAmmo > 0)
        {
            bulletCount = totalAmmo;
            totalAmmo = 0;
        }
        BulletText.text = bulletCount.ToString() + "/" + totalAmmo.ToString();
    }

    public void SetReloadingFalse()
    {
        anim.SetBool("isReloading", false);
    }
    public void SetTotalAmmo(float total)
    {
        totalAmmo = total;
        BulletText.text = bulletCount.ToString() + "/" + totalAmmo.ToString();
    }
    public float GetTotalAmmo()
    {
        return totalAmmo;
    }

    public void PlayLoadSound()
    {
        gunSound.PlayOneShot(load, .5f);

    }
    public void PlayUnloadSound()
    {
        gunSound.PlayOneShot(unload, .5f);
    }
}
