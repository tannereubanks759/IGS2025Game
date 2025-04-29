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
    public GameObject goGetTicketText;
    public float miniGameTime=0f;
    public int randomNumber;
    public GameObject[] grassAreas;
    public SphereCollider[] colliderToEnable;
    public balloonMinigame balloonMinigameRef;
    public bool firstQuest = false;
    public ParticleSystem[] grassHighlights;
    private minigameIcon minigameIconBal;
    private grassIconUpdater grassIconUpdater;
    private AudioSource audioSource;
    private bool balFirstTime = true;
    private bool grassFirstTime = true;
    public Animator animatorForUI;
    public Animator goGetTicketUIAnimator;
    //public GameObject borderOfQuestUI;
    [SerializeField] private GameObject playerUI;

    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minigameIconBal = FindAnyObjectByType<minigameIcon>();
        grassIconUpdater = FindAnyObjectByType<grassIconUpdater>();
        currentHeadShots = 0;
        ui.SetActive(false);
        questUI.SetActive(true);
        for (int i = 0; i < grassAreas.Length; i++)
        {
            grassAreas[i].tag = "Wrong Grass";
        }

        for (int i = 0; i < grassHighlights.Length; i++)
        {
            grassHighlights[i].Stop();
            grassHighlights[i].Clear();
        }
        goGetTicketText.SetActive(true);
        audioSource = playerUI.GetComponent<AudioSource>();
    }
    private void Awake()
    {
        instance = this;
        

    }
 
    public void printHs()
    {
        if(currentHeadShots==5)
        {
            currentScore.text = currentHeadShots.ToString();
            headShotQuest = false;
            resetQuest();

            audioSource.Play();
        }
        else
        {
            currentScore.text = currentHeadShots.ToString();
        }
        
        
    }
    public void resetQuest()
    {
        //questUI.SetActive(false);
        animatorForUI.SetTrigger("endQuest");
        currentHeadShots = 0;
        currentOutOfScore.text = " ";
        currentScore.text = "0";
        hasQuest = false;
        // gives negative of time taken
        miniGameTime -= Time.time;
        miniGameTime *=  -1;
        //Debug.Log("Sent call to function of time");
        ticketGiverScriptRef.calculateScore(miniGameTime);
        goGetTicketUIAnimator.SetTrigger("activate");
        //goGetTicketText.SetActive(true);
        ticketGiverScriptRef.canClaimTicket = true;
        areaScriptRef.timeInAreaMinigameOn = false;
        minigameIconBal.firstTime = false;
        grassIconUpdater.firstTime = false;
        
        
    }
    public void startMinigame()
    {
        firstQuest = true;
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
            if(grassFirstTime==true) 
            {
                grassIconUpdater.firstTime = true;
            }
            timeInArea();
        }
        else if(numberOfQuest == 2)
        {
            if (balFirstTime == true)
            {
                minigameIconBal.firstTime = true;
            }
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
        animatorForUI.SetTrigger("startQuest");
        
    }
    void timeInArea()
    {
        grassFirstTime = false;
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
        animatorForUI.SetTrigger("startQuest");
        
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

        PlayGrassHighlight(randomNumber);
        
    }
    void startBalloon()
    {
        balFirstTime = false;
        if (isChanged)
        {
            currentScore.rectTransform.anchoredPosition += new Vector2(+60f, 0f);
            isChanged = false;
        }
        currentOutOfScore.text = "/5";
        animatorForUI.SetTrigger("startQuest");
        
        balloonMinigameRef.startBalloon();
    }
    public void turnOffColliders()
    {
        for (int i = 0; i < colliderToEnable.Length; i++)
        {
            colliderToEnable[i].enabled = false;
        }
    }

    void PlayGrassHighlight(int i)
    {
        grassHighlights[i].Play();
    }

    public void StopGrassHighlight(int i)
    {
        grassHighlights[i].Stop();
        grassHighlights[i].Clear();
    }

}
