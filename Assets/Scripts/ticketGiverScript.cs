using UnityEngine;

public class ticketGiverScript : MonoBehaviour
{
    public miniGameScript miniGameScriptRef;
    public bool canClaimTicket = false;
    public GameObject interactUI;
    private bool inRangeToInteract = false;
    public AudioSource ticketSound;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)&&inRangeToInteract&&canClaimTicket) 
        {
            miniGameScriptRef.tickets++;
            miniGameScriptRef.ticketText.text = miniGameScriptRef.tickets.ToString();
            miniGameScriptRef.goGetTicketText.gameObject.SetActive(false);
            ticketSound.Play();
            canClaimTicket = false;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            interactUI.SetActive(true);
            inRangeToInteract=true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactUI.SetActive(false);
            inRangeToInteract=false;
        }
    } 
}
