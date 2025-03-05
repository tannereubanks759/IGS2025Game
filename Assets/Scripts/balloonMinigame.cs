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
    public Material goldenMatieral;
    public bool isMiniActive = false;
    public miniGameScript minigameRef;
    private int score =0;
    
    public List<GameObject> disabledBalloons;
    void Start()
    {
        sizeBall = balloons.Length;
        Renderer renderer = goldenBalloon.GetComponentInChildren<Renderer>();
        renderer.material = goldenMatieral;

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
       
    }
    private void UpdateBalloons(int number)
    {
        balloonToEdit = balloons[number];
        Instantiate(goldenBalloon, balloonToEdit.transform.position, balloonToEdit.transform.rotation);
        disabledBalloons.Add(balloonToEdit);
        balloonToEdit.SetActive(false);
    }
    public void shotRightBalloon()
    {
        score++;
        minigameRef.currentScore.text = score.ToString();
        numberOfGoldenBalloons--;
        if(numberOfGoldenBalloons==0)
        {
            Debug.Log("Finished quest");
            Invoke("resetBalloons", 3);
            minigameRef.resetQuest();
            //resetBalloons();
        }
    }
    public void shotWrongBalloon()
    {
        Debug.Log("Wrong");
    }
    public void resetBalloons()
    {
        for(int i = 0; i<disabledBalloons.Count; i++) 
        {
            Debug.Log("Enabled balloon" + i);
            disabledBalloons[i].SetActive(true);
        }
    }
}
