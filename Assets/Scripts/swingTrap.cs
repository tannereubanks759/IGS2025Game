using UnityEngine;

public class swingTrap : MonoBehaviour
{
    //public Animator animator;
    public Animator trapAnim;
    bool canInteract = false;
    public GameObject interactUI;
    
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
            trapAnim.SetBool("isOn", true);
            
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
}
