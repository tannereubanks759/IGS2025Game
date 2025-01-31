using Unity.AppUI.UI;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class miniGameScript : MonoBehaviour
{
    public GameObject ui;
    public GameObject questUI;
    public bool isInteractable = false;
    private bool hasQuest = false;
    public string quest;
    public TextMeshProUGUI questText;
    public int currentHeadShots = 0;
    public bool headShotQuest = false;
    //private int prevHs = 0;
    public string[] quests = { "Get 8 headshots", "Get kills within indicated zone", "Get 5 kills without reloading " };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHeadShots = 0;
        ui.SetActive(false);
        questUI.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInteractable && hasQuest == false)
        {
            startMinigame();
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            hasQuest = false;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && hasQuest == false)
        {
            //Debug.Log("Entered zone");
            ui.SetActive(true);
            isInteractable=true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Left Zone");
            ui.SetActive(false);
            isInteractable=false;
        }
    }
    public void printHs()
    {
        Debug.Log("Current headshot count: ");
        Debug.Log(currentHeadShots);
    }
    void startMinigame()
    {
        //float gameNumber = Random.value;
        
        int number = Random.Range( 0,  quests.Length);
        //Debug.Log(number);
        hasQuest = true;
        quest = quests[number];
        questStarter(quest, number);
        //Debug.Log(quest);
    }
    void questStarter(string textFromArray, int numberOfQuest)
    {
        //bool isGoal = false;
        questText.text = textFromArray;
        questUI.SetActive(true);
        if (numberOfQuest == 1)
        {
            headShot();
        }
        else if (numberOfQuest == 2) 
        {
            timeInArea();
        }
    }
    void headShot()
    {
        Debug.Log("headshot quest");
        headShotQuest = true;
    }
    void timeInArea()
    {
        Debug.Log("time quest");
        // tracks time in area with colliders
    }
        


    
    
}
