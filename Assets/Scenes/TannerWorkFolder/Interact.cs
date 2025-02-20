using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject InteractText;
    public AudioSource SoundEffects;
    public AudioClip RefillAmmoSound;
    public miniGameScript minigameScriptRef;
    public ticketGiverScript ticketGiverScriptRef;
    public swingTrap swingTrapRef;
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
        if(other.gameObject.tag == "Minigame Starter")
        {
            InteractText.SetActive(true);
            if (Input.GetKey(KeyCode.E) && minigameScriptRef.isInteractable && minigameScriptRef.hasQuest == false && ticketGiverScriptRef.hasTaken == true)
            {
                minigameScriptRef.startMinigame();
            }
        }
        if(other.gameObject.tag == "trap starter")
        {
            InteractText.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                swingTrapRef.startTrap();
            }
        }
        if(other.gameObject.tag== "ticketGiver")
        {
            InteractText.SetActive(true);
            if (Input.GetKey(KeyCode.E) && ticketGiverScriptRef.inRangeToInteract && ticketGiverScriptRef.canClaimTicket)
            {
                ticketGiverScriptRef.giveTicket();
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        InteractText.SetActive(false);
    }
}
