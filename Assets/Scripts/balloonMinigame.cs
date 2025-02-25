using UnityEngine;

public class balloonMinigame : MonoBehaviour
{
    
    public GameObject[] balloons;
    public GameObject redBalloon;
    public GameObject greenBalloon;
    public GameObject blueBalloon;
    private int number;
    public int color;
    public string colorTag;
    void Start()
    {
        int size = balloons.Length; 
    }

    // Update is called once per frame
    void Update()
    {
        // lights off, array of all red, lights on, randomly choose parts of the red to change material to gold, then shooting logic
    }
    public void startBalloon()
    {
         color = generateColor();
        if(color==1)
        {
            colorTag = "Red";
            
        }
        else if(color==2) 
        {
            colorTag = "Blue";
            
        }
        else
        {
            colorTag = "Yellow";
            
        }
    }
    private int generateColor()
    {
        number = Random.Range(1, 3);
        return number;
    }
    public void shotRightBalloon()
    {

    }
    public void shotWrongBalloon()
    {

    }
}
