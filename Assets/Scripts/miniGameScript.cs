using Unity.AppUI.UI;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class miniGameScript : MonoBehaviour
{
    #region Variables
    public GameObject ui;
    public GameObject questUI;
    public bool isInteractable = false;
    public bool hasQuest = false;
    public string quest;
    public TextMeshProUGUI questText;
    public int currentHeadShots = 0;
    public bool headShotQuest = false;
    public static miniGameScript instance;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI currentOutOfScore;
    public TextMeshProUGUI ticketText;
    public int tickets =0;
    public float totalTime = 10f;
    public string[] quests;
    public timeInAreaScript areaScriptRef;
    private bool isChanged = false;
    public ticketGiverScript ticketGiverScriptRef;
    public TextMeshProUGUI goGetTicketText;
    public float miniGameTime=0f;
    public int randomNumber;
    public GameObject[] grassAreas;
    public SphereCollider[] colliderToEnable;
    public balloonMinigame balloonMinigameRef;
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHeadShots = 0;
        ui.SetActive(false);
        questUI.SetActive(false);
        for (int i = 0; i < grassAreas.Length; i++)
        {
            grassAreas[i].tag = "Wrong Grass";
        }

    }
    private void Awake()
    {
        instance = this;
        

    }

    // Update is called once per frame
    void Update()
    {
       
        
        
    }
   
    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
 
    public void printHs()
    {
        if(currentHeadShots==5)
        {
            currentScore.text = currentHeadShots.ToString();
            headShotQuest = false;
            resetQuest();

        }
        else
        {
            currentScore.text = currentHeadShots.ToString();
        }
        
        
    }
    public void resetQuest()
    {
        questUI.SetActive(false);
        currentHeadShots = 0;
        currentOutOfScore.text = " ";
        currentScore.text = "0";
        hasQuest = false;
        // gives negative of time taken
        miniGameTime -= Time.time;
        miniGameTime *=  -1;
        Debug.Log("Sent call to function of time");
        ticketGiverScriptRef.calculateScore(miniGameTime);
        goGetTicketText.gameObject.SetActive(true);
        ticketGiverScriptRef.canClaimTicket = true;
        areaScriptRef.timeInAreaMinigameOn = false;


    }
    public void startMinigame()
    {
        
        miniGameTime = Time.time;
        ticketGiverScriptRef.hasTaken = false;
        int number = Random.Range( 0,  quests.Length);
        
        hasQuest = true;
        quest = quests[number];
        questStarter(quest, number);
        
        
    }
    void questStarter(string textFromArray, int numberOfQuest)
    {
        
        questText.text = textFromArray;
        questUI.SetActive(true);
        if (numberOfQuest == 0)
        {
            headShot();
        }
        else if (numberOfQuest == 1) 
        {
            timeInArea();
        }
        else if(numberOfQuest == 2)
        { 
            startBalloon();
        }
    }
    void headShot()
    {
        Debug.Log("headshot quest");
        if(isChanged)
        {
            currentScore.rectTransform.anchoredPosition += new Vector2(+60f, 0f);
            isChanged = false;
        }
        headShotQuest = true;
        currentOutOfScore.text = "/5";
    }
    void timeInArea()
    {
        Debug.Log("time quest");
        if(isChanged==false)
        {
            currentScore.rectTransform.anchoredPosition += new Vector2(-60f, 0f);
            isChanged = true;
        }
        determineGrass();
        areaScriptRef.timeInAreaMinigameOn = true;
        currentOutOfScore.text = "/10";
        currentScore.text = " ";

        // tracks time in area with colliders
    }

     void determineGrass()
    {
        randomNumber = Random.Range(0, grassAreas.Length);
        colliderToEnable = grassAreas[randomNumber].GetComponents<SphereCollider>();
        for (int i = 0; i < colliderToEnable.Length; i++)
        {
            colliderToEnable[i].enabled = true;
        }
        
    }
    void startBalloon()
    {
        currentOutOfScore.text = "/5";
        
        balloonMinigameRef.startBalloon();
    }
    public void turnOffColliders()
    {
        for (int i = 0; i < colliderToEnable.Length; i++)
        {
            colliderToEnable[i].enabled = false;
        }
    }

}
