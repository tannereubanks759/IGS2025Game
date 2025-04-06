using Unity.Collections;
using UnityEngine;

public class Truck : MonoBehaviour
{
    public GameObject[] truckWheels;
    public int price;
    public string InteractionString;
    public GameObject WinScreen;
    private int numberofwheelsrepaired;
    private void Start()
    {
        InteractionString = "Repair For 10 Tickets (0/4)";
        numberofwheelsrepaired = 0;
        for(int i = 0; i < truckWheels.Length; i++)
        {
            truckWheels[i].SetActive(false);
        }
    }
    public void Repair()
    {
        miniGameScript mini = GameObject.FindAnyObjectByType<miniGameScript>();
        if (mini.tickets >= price && numberofwheelsrepaired < truckWheels.Length)
        {
            GameObject.FindAnyObjectByType<Interact>().PlayPurchaseSound();
            numberofwheelsrepaired++;
            InteractionString = "Repair For 5 Tickets (" + numberofwheelsrepaired + "/4)";
            mini.tickets -= price;
            mini.ticketText.text = mini.tickets.ToString();
            for(int i = 0; i < truckWheels.Length; i++)
            {
                if (!truckWheels[i].activeSelf)
                {
                    truckWheels[i].SetActive(true);
                    if(numberofwheelsrepaired == truckWheels.Length)
                    {
                        GameObject.FindAnyObjectByType<CutsceneScript>().PlayCutscene();
                        //ENDGAME();
                    }
                    break;
                }
                
                
            }
        }
    }

    public void ENDGAME()
    {
        //GameObject.FindAnyObjectByType<FirstPersonController>().cameraCanMove = false;
        WinScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0.0f;

    }
}
