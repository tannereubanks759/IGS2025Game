
using UnityEngine;
using UnityEngine.Rendering;

public class timeInAreaScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool timeInAreaMinigameOn = false;
    float timeInArea = 0f;
    public miniGameScript miniGameScript;
    private bool inZone = false;
    public GameObject[] grassAreas;
    public int randomNumber;
    void Start()
    {
       for (int i = 0; i< grassAreas.Length; i++)
        {
            grassAreas[i].tag = "Wrong Grass";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(inZone)
        {
            if (timeInArea < miniGameScript.totalTime)
            {
                timeInArea += Time.deltaTime;
                miniGameScript.currentScore.text = timeInArea.ToString("F2");
                
            }
            
        }
    }
    public void determineGrass()
    {
        randomNumber = Random.Range(1, grassAreas.Length);
        grassAreas[randomNumber].gameObject.tag = "Active Grass";
    }
    private void OnTriggerEnter(Collider other)
    {
       
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && timeInAreaMinigameOn&& this.gameObject.CompareTag("Active Grass"))
        {
            inZone = true;
            if (timeInArea >= miniGameScript.totalTime)
            {
                timeInAreaMinigameOn = false;
                miniGameScript.resetQuest();
                this.gameObject.tag = "Wrong Grass";
                timeInArea = 0f;
                inZone = false;
            }


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inZone = false;
           

        }
    }
}

