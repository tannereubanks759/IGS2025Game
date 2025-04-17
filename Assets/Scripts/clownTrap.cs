using UnityEngine;

public class clownTrap : MonoBehaviour
{
    public int costPrice = 1;
    public bool paid = false;
    public miniGameScript miniGameObject;
    public ClownRideMovement clownRideMovementRef;
    public Animator clownRideAnim;
    private string nameOfFunction = "startMovement";

    // mats to show if trap is able to be bought or not
    public GameObject onMat;
    public GameObject offMat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        miniGameObject = FindAnyObjectByType<miniGameScript>();

        onMat.SetActive(true);
        offMat.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startClownTrap()
    {
        if (miniGameObject.tickets >= costPrice && paid == false)
        {
            clownRideAnim.SetBool("isTrapActive", true);
            paid = true;
            GameObject.FindAnyObjectByType<Interact>().PlayPurchaseSound();
            miniGameObject.tickets -= costPrice;
            miniGameObject.ticketText.text = miniGameObject.tickets.ToString();

            // turn the onButton off and the offButton on
            onMat.SetActive(false);
            offMat.SetActive(true);

            Invoke(nameOfFunction, 1.15f);
        }
    }
    private void startMovement()
    {
        
        clownRideMovementRef.isActive = true;
        
        
    }
}
