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
    void Start()
    {
        isGunAway = true;
        isMiniAway = true;
        BuffOver = true;
        postProcessing.GetComponent<Volume>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.V))
        {
            ActivateBuff();
        }*/
        if (isGunAway&&isMiniAway)
        {
            if (BuffOver)
            {
                isGunAway = false;
                gun.gameObject.SetActive(true);
                minigun.gameObject.SetActive(false);
                postProcessing.GetComponent<Animator>().SetBool("isActive", false);
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
        gun.anim.SetBool("drop", true);
    }

}
