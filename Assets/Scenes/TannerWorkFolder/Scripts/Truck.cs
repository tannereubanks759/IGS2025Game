using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Truck : MonoBehaviour
{
    public GameObject[] truckWheels;
    public int price;
    public string InteractionString;
    public GameObject WinScreen;
   // private int numberofwheelsrepaired;
    public Animator uiWinAnimator;
    public int killsToWin;
    public bool isFirstTime = false;

    private void Start()
    {
        InteractionString = "0/170 Kills To Unlock";
        //numberofwheelsrepaired = 0;
        /*for(int i = 0; i < truckWheels.Length; i++)
        {
            truckWheels[i].SetActive(false);
        }*/
    }
    private void Update()
    {
        InteractionString = ZombieManager.totalZombiesKilled + "/170 Kills To Unlock";
        if(ZombieManager.totalZombiesKilled >=killsToWin&&isFirstTime==false)
        {
            InteractionString = "Escape Carnival";
            uiWinAnimator.SetTrigger("escapeUI");
            isFirstTime = true;
            //uiWinAnimator.SetBool("firstTime",true);
        }

    }
    public void Repair()
    {
        //miniGameScript mini = GameObject.FindAnyObjectByType<miniGameScript>();
        if (ZombieManager.totalZombiesKilled>=killsToWin)
        {
            GameObject.FindAnyObjectByType<CutsceneScript>().PlayCutscene();
            //GameObject.FindAnyObjectByType<Interact>().PlayPurchaseSound();
            //numberofwheelsrepaired++;

            //mini.tickets -= price;
            //mini.ticketText.text = mini.tickets.ToString();
            /*for(int i = 0; i < truckWheels.Length; i++)
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
                
                
            }*/
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
