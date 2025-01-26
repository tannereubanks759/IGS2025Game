using Unity.AppUI.UI;
using Unity.VisualScripting;
using UnityEngine;

public class miniGameScript : MonoBehaviour
{
    public GameObject ui;
    public bool isInteractable = false;
    private bool hasQuest = false;
    public string quest;
    public string[] quests = { "Get 8 headshots", "Get kills within indicated zone", "Get 5 kills without reloading " };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ui.SetActive(false);
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
            Debug.Log("Entered zone");
            ui.SetActive(true);
            isInteractable=true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Left Zone");
            ui.SetActive(false);
            isInteractable=false;
        }
    }
    void startMinigame()
    {
        //float gameNumber = Random.value;
        
        int number = Random.Range( 0,  quests.Length);
        Debug.Log(number);
        hasQuest = true;
        quest = quests[number];
        Debug.Log(quest);
    }
    
}
