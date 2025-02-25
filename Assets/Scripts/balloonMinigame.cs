using UnityEngine;

public class balloonMinigame : MonoBehaviour
{
    public GameObject[] balloons;
    public GameObject redBalloon;
    public GameObject greenBalloon;
    public GameObject blueBalloon;
    void Start()
    {
        int size = balloons.Length; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startBalloon()
    {
        int color = generateColor();
        if(color==1)
        {
            Debug.Log("Red");
        }
        else if(color==2) 
        {
            Debug.Log("Blue");
        }
        else
        {
            Debug.Log("Yellow");
        }
    }
    private int generateColor()
    {
        int number = Random.Range(1, 3);
        return number;
    }
    public void shotBalloon()
    {

    }
}
