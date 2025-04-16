using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class balloonMinigame : MonoBehaviour
{
    
    public GameObject[] balloons;
    private GameObject balloonToEdit;
    public int number;
    private int sizeBall;
    public int numberOfGoldenBalloons;
    private int positionOne;
    public List<int> prevNumbers;
    public GameObject goldenBalloon;
    //public Material goldenMatieral;
    public bool isMiniActive = false;
    public miniGameScript minigameRef;
    private int score =0;
    public Material redBalloonMaterial;
    public Material goldenBalloonMatieral;
    //public List<GameObject> disabledBalloons;

    private AudioSource audioSource;
    [SerializeField] private GameObject playerUI;

    void Start()
    {
        minigameRef = FindAnyObjectByType<miniGameScript>();
        sizeBall = balloons.Length;
        /*Renderer renderer = goldenBalloon.GetComponentInChildren<Renderer>();
        renderer.material = goldenBalloonMatieral;*/
        goldenBalloonMatieral.color = Color.yellow;

        audioSource = playerUI.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)) { startBalloon(); }
        // lights off, array of all red, lights on, randomly choose parts of the red to change material to gold, then shooting logic
    }
    public void startBalloon()
    {
        isMiniActive = true;
        for (int i = 0; i<numberOfGoldenBalloons;i++)
        {
            positionOne = generateGoldenBalloonLocations();
            UpdateBalloons(positionOne);
        }
        
        
    }
    private int generateGoldenBalloonLocations()
    {

        if(prevNumbers!=null) 
        {
            number = Random.Range(0, sizeBall);
            while (prevNumbers.Contains(number))
            {
                //Debug.Log("Generated a duplicate number of" + number);
                number = Random.Range(0, sizeBall);

            }
            prevNumbers.Add(number);

            return number;
        }
        else
        {
            number = Random.Range(0, sizeBall);
            prevNumbers.Add(number);
            return number;
        }
        /*int number = -1;
        while(number == -1)
        {
            int random = Random.Range(0, sizeBall);
            if (!prevNumbers.Contains(random))
            {
                number = random;
                prevNumbers.Add(number);
                return (number);
            }
            
        }
        return (0);*/
        
    }
    private void UpdateBalloons(int number)
    {
        goldenBalloonMatieral.color = Color.yellow;
        balloonToEdit = balloons[number];
        balloonToEdit.GetComponentInChildren<MeshRenderer>().material = goldenBalloonMatieral;
        //Instantiate(goldenBalloon, balloonToEdit.transform.position, balloonToEdit.transform.rotation);

        /*disabledBalloons.Add(balloonToEdit);
        balloonToEdit.SetActive(false);*/
    }
    public void shotRightBalloon(Collision collision)
    {
        
        if(collision.gameObject.GetComponentInChildren<MeshRenderer>().material.color ==  Color.yellow)
        {
            score++;
            minigameRef.currentScore.text = score.ToString();
            numberOfGoldenBalloons--;
            if (numberOfGoldenBalloons == 0)
            {
                Debug.Log("Finished quest");
                resetBalloons();
                minigameRef.resetQuest();

                audioSource.Play();

                //resetBalloons();
            }
        }
        else
        {
            /*Debug.Log(goldenMatieral.name);
            Debug.Log("Material detected: " + collision.gameObject.GetComponentInChildren<MeshRenderer>().material.name);*/
        }
        
    }
   
    public void resetBalloons()
    {
        isMiniActive = false;
        for(int i = 0; i < balloons.Length; i++)
        {
           if( balloons[i].activeSelf ==false)
            {
                balloons[i].SetActive(true);
                balloons[i].GetComponentInChildren<MeshRenderer>().material = redBalloonMaterial;    
            }
        }
        prevNumbers.Clear();
        numberOfGoldenBalloons = 5;
        score = 0;
    }
}
