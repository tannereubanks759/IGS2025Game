using UnityEngine;

public class FoodTruck : MonoBehaviour
{
    //public variables
    public int price;
    public string priceTextString;
    public bool hamburger;
    public bool hotdog;
    public bool icecream;
    public bool donut;

    //private variables
    private miniGameScript ticketRef;
    private GunScript gun;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ticketRef = GameObject.FindFirstObjectByType<miniGameScript>();
        gun = GameObject.FindFirstObjectByType<GunScript>();
        updatePrice();
    }

    public void updatePrice()
    {
        if (donut)
        {
            priceTextString = "Buy 1.5X Reload Speed For " + price + " Tickets";
        }
        if (hamburger)
        {
            priceTextString = "Buy Increased Damage For " + price + " Tickets";
        }
        if (icecream)
        {
            priceTextString = "Buy +30 Magazine Size For " + price + " Tickets";
        }
        if (hotdog)
        {
            priceTextString = "Buy +200 Bullet Stash For " + price + " Tickets";
        }
    }
    public void BuyPerk()
    {
        if(ticketRef.tickets >= price)
        {
            ticketRef.tickets -= price;
            ticketRef.ticketText.text = ticketRef.tickets.ToString();
            Donut();
            Hamburger();
            Icecream();
            Hotdog();
            price = price + 10;
            updatePrice();
        }
    }

    void Donut()
    {
        if (donut)
        {
            gun.reloadSpeed *= 1.5f;
            Debug.Log("Purchased Donut");
        }
    }

    void Hamburger()
    {
        if (hamburger)
        {
            BulletScript.baseDamage += 1;
            Debug.Log(BulletScript.baseDamage);
            Debug.Log("Purchased Hamburger");
        }
    }

    void Icecream()
    {
        if (icecream)
        {
            gun.magazineSize += 30f;
            gun.Reload();
            Debug.Log("Purchased Icecream");
        }
    }

    void Hotdog()
    {
        if (hotdog)
        {
            gun.maxAmmo += 200f;
            gun.SetTotalAmmo();
            Debug.Log("Purchased Hotdog");
        }
    }
}
