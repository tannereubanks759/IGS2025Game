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
    public clownTrap clownTrapRef;
    void Start()
    {
        minigameScriptRef = FindAnyObjectByType<miniGameScript>();
        InteractText.SetActive(false);
    }

    

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == "Ammo")
        {
            InteractText.SetActive(true);
            if (minigameScriptRef.tickets>0 && Input.GetKeyDown(KeyCode.E) && GetComponentInChildren<GunScript>().GetTotalAmmo() < GetComponentInChildren<GunScript>().maxAmmo)
            {
                minigameScriptRef.tickets -= 1;
                minigameScriptRef.ticketText.text = minigameScriptRef.tickets.ToString();
                GetComponentInChildren<GunScript>().SetTotalAmmo();
                SoundEffects.PlayOneShot(RefillAmmoSound, .5f);
            }
        }
        else if (other.gameObject.tag == "Truck")
        {
            Truck T = other.GetComponent<Truck>();
            InteractText.SetActive(true);
            InteractText.GetComponent<TextMeshProUGUI>().text = T.InteractionString;
            if (Input.GetKeyDown(KeyCode.E))
            {
                T.Repair();
            }
        }
        else if(other.gameObject.tag == "Minigame Starter" && other.gameObject.layer == 18)
        {
            InteractText.SetActive(true);
            if (Input.GetKey(KeyCode.E) && ticketGiverScriptRef.canClaimTicket)
            {
                ticketGiverScriptRef.giveTicket();
            }
            if (Input.GetKey(KeyCode.E) && minigameScriptRef.hasQuest == false && ticketGiverScriptRef.hasTaken == true)
            {
                Debug.Log("Start Minigame");
                minigameScriptRef.startMinigame();
            }

            
        }
        else if(other.gameObject.tag == "trap starter")
        {
            InteractText.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                swingTrapRef.startTrap();
            }
        }
        else if(other.gameObject.tag == "coaster trap starter")
        {
            InteractText.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                coasterRef.startCoaster();
            }
        }
        else if(other.gameObject.tag == "FoodStand")
        {
            InteractText.SetActive(true);
            FoodTruck truck = other.GetComponent<FoodTruck>();
            InteractText.GetComponent<TextMeshProUGUI>().text = truck.priceTextString;
            if (Input.GetKeyDown(KeyCode.E))
            {
                truck.BuyPerk();
            }
        }
        else if(other.gameObject.tag == "clown trap starter")
        {
            InteractText.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                clownTrapRef.startClownTrap();
            }
        }
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "FoodStand" || other.gameObject.tag == "Truck")
        {
            InteractText.GetComponent<TextMeshProUGUI>().text = "Interact (E)";
        }
        InteractText.SetActive(false);
    }
}
