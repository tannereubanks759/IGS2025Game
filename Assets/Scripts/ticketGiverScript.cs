using UnityEngine;

public class ticketGiverScript : MonoBehaviour
{
    public miniGameScript miniGameScriptRef;
    public bool canClaimTicket = false;
    public GameObject interactUI;
    public  bool inRangeToInteract = false;
    public AudioSource ticketSound;
    public bool hasTaken = true;
    float score = 0f;
    int scoreInt = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.E)&&inRangeToInteract&&canClaimTicket) 
        {
            miniGameScriptRef.tickets++;
            miniGameScriptRef.ticketText.text = miniGameScriptRef.tickets.ToString();
            miniGameScriptRef.goGetTicketText.gameObject.SetActive(false);
            ticketSound.Play();
            canClaimTicket = false;
            hasTaken = true;
        }*/

        if (Input.GetKeyDown(KeyCode.T)) //For debugging purposes
        {
            miniGameScriptRef.tickets += 1;
            miniGameScriptRef.ticketText.text = miniGameScriptRef.tickets.ToString();
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
    public void giveTicket()
    {
        miniGameScriptRef.tickets += scoreInt;
        miniGameScriptRef.ticketText.text = miniGameScriptRef.tickets.ToString();
        miniGameScriptRef.goGetTicketText.gameObject.SetActive(false);
        ticketSound.Play();
        canClaimTicket = false;
        hasTaken = true;
    }
    public void calculateScore(float timeTaken)
    {
        Debug.Log("Time taken to complete quest: " + timeTaken);
        score = 100f / timeTaken;
        scoreInt =  Mathf.RoundToInt(score);
        if(scoreInt <1)
        {
            scoreInt = 1;
        }
    }
}
