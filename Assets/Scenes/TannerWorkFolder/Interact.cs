using TMPro;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject InteractText;
    public AudioSource SoundEffects;
    public AudioClip RefillAmmoSound;
    public miniGameScript minigameScriptRef;
    public ticketGiverScript ticketGiverScriptRef;
    public swingTrap swingTrapRef;
    public KiddieCoaster coasterRef;
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
            if (Input.GetKey(KeyCode.E) && GetComponentInChildren<GunScript>().GetTotalAmmo() < GetComponentInChildren<GunScript>().maxAmmo)
            {
                GetComponentInChildren<GunScript>().SetTotalAmmo();
                SoundEffects.PlayOneShot(RefillAmmoSound, .5f);
            }
        }
        if(other.gameObject.tag == "Minigame Starter")
        {
            InteractText.SetActive(true);
            if (Input.GetKey(KeyCode.E) && minigameScriptRef.hasQuest == false && ticketGiverScriptRef.hasTaken == true)
            {
                Debug.Log("Start Minigame");
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
        if(other.gameObject.tag == "coaster trap starter")
        {
            InteractText.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                coasterRef.startCoaster();
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
        if(other.gameObject.tag == "FoodStand")
        {
            InteractText.SetActive(true);
            FoodTruck truck = other.GetComponent<FoodTruck>();
            InteractText.GetComponent<TextMeshProUGUI>().text = truck.priceTextString;
            if (Input.GetKeyDown(KeyCode.E))
            {
                truck.BuyPerk();
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "FoodStand")
        {
            InteractText.GetComponent<TextMeshProUGUI>().text = "Interact (E)";
        }
        InteractText.SetActive(false);
    }
}
