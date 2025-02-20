
using UnityEngine;
using UnityEngine.Rendering;

public class timeInAreaScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool timeInAreaMinigameOn = false;
    float timeInArea = 0f;
    public miniGameScript miniGameScript;
    private bool inZone = false;
    void Start()
    {
       
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
                Debug.Log(timeInArea);
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")&& timeInAreaMinigameOn)
        {
            Debug.Log("Player entered");
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && timeInAreaMinigameOn)
        {
            inZone = true;
            if (timeInArea >= miniGameScript.totalTime)
            {
                timeInAreaMinigameOn = false;
                miniGameScript.resetQuest();
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

