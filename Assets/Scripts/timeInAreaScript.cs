
using UnityEngine;
using UnityEngine.Rendering;

public class timeInAreaScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool timeInAreaMinigameOn = false;
    float timeInArea = 0f;
    public miniGameScript miniGameScript;
    private bool inZone = false;

    private AudioSource audioSource;
    [SerializeField] private GameObject playerUI;



    void Start()
    {
        miniGameScript = FindAnyObjectByType<miniGameScript>();

        audioSource = playerUI.GetComponent<AudioSource>();
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
  
    private void OnTriggerEnter(Collider other)
    {
       
        
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            inZone = true;
            if (timeInArea >= miniGameScript.totalTime)
            {
                audioSource.Play();

                timeInAreaMinigameOn = false;
                miniGameScript.resetQuest();
                //this.gameObject.tag = "Wrong Grass";
                miniGameScript.turnOffColliders();
                miniGameScript.StopGrassHighlight(miniGameScript.randomNumber);
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

