using UnityEngine;
using UnityEngine.Rendering;

public class GunHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isGunAway;
    public bool isMiniAway;
    public GunScript gun;
    public MinigunScript minigun;
    public bool BuffOver;
    public GameObject postProcessing;
    public GameObject currentSpawnedBuff;
    public int chanceToSpawnBuff = 1;
    public MusicManager music;
    void Start()
    {
        currentSpawnedBuff = null;
        isGunAway = true;
        isMiniAway = true;
        BuffOver = true;
        postProcessing.GetComponent<Volume>().enabled = false;
        chanceToSpawnBuff = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    ActivateBuff();
        //}
        if (isGunAway&&isMiniAway)
        {
            if (BuffOver)
            {
                isGunAway = false;
                gun.gameObject.SetActive(true);
                minigun.gameObject.SetActive(false);
                postProcessing.GetComponent<Animator>().SetBool("isActive", false);
                music.PlayNextSong();
            }
            else
            {
                isMiniAway = false;
                minigun.gameObject.SetActive(true);
                gun.gameObject.SetActive(false);
                postProcessing.GetComponent<Animator>().SetBool("isActive", true);
            }
        }
        
    }

    public void ActivateBuff()
    {
        postProcessing.GetComponent<Volume>().enabled = true;
        music.PlayBuffMusic();
        gun.anim.SetBool("drop", true);
    }

}
