using UnityEngine;

public class clownTrap : MonoBehaviour
{
    public int costPrice = 1;
    public bool paid = false;
    public miniGameScript miniGameObject;
    public ClownRideMovement clownRideMovementRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startClownTrap()
    {
        if (miniGameObject.tickets >= costPrice && paid == false)
        {
            clownRideMovementRef.isActive = true;
            paid = true;
            miniGameObject.tickets -= costPrice;
            miniGameObject.ticketText.text = miniGameObject.tickets.ToString();
        }
    }
}
