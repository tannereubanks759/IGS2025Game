using TMPro;
using UnityEngine;

public class BuyWall : MonoBehaviour
{
    public int price = 5;
    public string InteractTextOveride = "Remove Barrier (5 Tickets)";
    private bool isBought;
    public GameObject otherWall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isBought = false;
    }

    public void buy()
    {
        miniGameScript mini = GameObject.FindAnyObjectByType<miniGameScript>();
        if(isBought == false && mini.tickets >= price)
        {
            Interact cam = GameObject.FindAnyObjectByType<Interact>();
            cam.PlayPurchaseSound();
            cam.InteractText.SetActive(false);
            cam.InteractText.GetComponent<TextMeshProUGUI>().text = "Interact (E)";
            otherWall.SetActive(false);
            mini.tickets -= price;
            mini.ticketText.text = mini.tickets.ToString();
            isBought = true;
            this.gameObject.SetActive(false);
        }
        
    }
}
