using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject InteractText;
    public AudioSource SoundEffects;
    public AudioClip RefillAmmoSound;
    void Start()
    {
        InteractText.SetActive(false);
    }

    
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Ammo")
        {
            InteractText.SetActive(true);
            if (Input.GetKey(KeyCode.E) && GetComponentInChildren<GunScript>().GetTotalAmmo() < 400)
            {
                GetComponentInChildren<GunScript>().SetTotalAmmo(400);
                SoundEffects.PlayOneShot(RefillAmmoSound, .5f);
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        InteractText.SetActive(false);
    }
}
