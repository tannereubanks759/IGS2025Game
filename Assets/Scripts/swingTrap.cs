using UnityEngine;

public class swingTrap : MonoBehaviour
{
    //public Animator animator;
    public Animator trapAnim;
    //public bool canInteract = false;
    public GameObject interactUI;
    public miniGameScript miniGameObject;
    public int costPrice;
    
     public bool paid=false;    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactUI.SetActive(false);
    }
    

    // Update is called once per frame
    void Update()
    {
        
        /*if (Input.GetKeyDown(KeyCode.E)) 
        {
            if(miniGameObject.tickets >= costPrice)
            {
                
                trapAnim.SetBool("isOn", true);
                miniGameObject.tickets -= costPrice;
                miniGameObject.ticketText.text = miniGameObject.tickets.ToString();
            }
            else
            {
                Debug.Log("Not enough money");
            }

        }*/
        
       
        
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        canInteract = true;
        interactUI.SetActive(true);
    }
    private void OnCollisionExit(Collision collision)
    {
        canInteract = false;
        
        interactUI.SetActive(false);
    }*/
    public void resetAnimationTrigger()
    {
        trapAnim.SetBool("isOn", false);
        
    }
    public void startTrap()
    {
        if (miniGameObject.tickets >= costPrice&&paid==false)
        {
            paid = true;
            trapAnim.SetBool("isOn", true);
            miniGameObject.tickets -= costPrice;
            miniGameObject.ticketText.text = miniGameObject.tickets.ToString();
        }
        
    }
}
