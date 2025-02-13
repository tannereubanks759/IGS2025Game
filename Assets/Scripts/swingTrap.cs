using UnityEngine;

public class swingTrap : MonoBehaviour
{
    //public Animator animator;
    public Animator trapAnim;
    bool canInteract = false;
    public GameObject interactUI;
    public miniGameScript miniGameObject;
    public int costPrice;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactUI.SetActive(false);
    }
    

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E)&& canInteract) 
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

        }
        
        if(Input.GetKeyUp(KeyCode.G))
        {
            trapAnim.SetBool("isOn", false);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        canInteract = true;
        interactUI.SetActive(true);
    }
    private void OnCollisionExit(Collision collision)
    {
        canInteract = false;
        
        interactUI.SetActive(false);
    }
    public void resetAnimationTrigger()
    {
        trapAnim.SetBool("isOn", false);
    }
}
