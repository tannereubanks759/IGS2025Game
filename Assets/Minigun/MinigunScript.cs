
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;
using TMPro;
public class MinigunScript : MonoBehaviour
{
    public GameObject bulletSpawnPoint;
    public GameObject bullet;
    public KeyCode ShootKey;
    public VisualEffect muzzleFlash;
    //public float fireRate;
    public Animator anim; //booleans: isWalking isRunning isAiming isFiring isReloading
    public FirstPersonController playerScript;
    public GameObject crosshair;

    public float velocityThresh;
    public TextMeshProUGUI BulletText;

    //sounds
    public AudioSource gunSound;
    //public AudioClip gunShot;
    public AudioClip[] gunShots;
    public Animator bulletAnim;

    public static bool isFiring;

    private float nextFire;
    public GunHandler handler;
    public float buffTime = 30f;
    void Start()
    {
        nextFire = Time.time + buffTime;
        isFiring = false;
        BulletText.text = "999/999";
        crosshair.SetActive(true);
    }
    private void OnEnable()
    {
        nextFire = Time.time + buffTime;
    }
    void Update()
    {
        //anim control for walking and running
        if (playerScript.isSprinting && playerScript.rb.linearVelocity.magnitude > velocityThresh)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }



        //anim for shooting
        if (playerScript.cameraCanMove && Input.GetKey(ShootKey))
        {
            anim.SetBool("isFiring", true);
            bulletAnim.SetBool("fire", true);
            isFiring = true;
        }
        else
        {
            anim.SetBool("isFiring", false);
            bulletAnim.SetBool("fire", false);
            isFiring = false;
        }
        BulletText.text = "999/999";

        if(Time.time > nextFire)
        {
            anim.SetBool("drop", true);
        }

    }
    public void Fire()
    {
        GameObject bulletObj = Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        bulletObj.GetComponent<BulletScript>().isMiniBullet = true;
        muzzleFlash.Play();
        gunSound.pitch = Random.Range(1.5f, 1.9f);
        AudioClip CLIP = gunShots[Random.Range(0, gunShots.Length)];
        gunSound.PlayOneShot(CLIP, 1f);
    }
    public void PutAway()
    {
        handler.isMiniAway = true;
        handler.BuffOver = true;
    }
}
