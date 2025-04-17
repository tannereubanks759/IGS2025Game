using Unity.AppUI.Editor;
using UnityEngine;

public class minigameIcon : MonoBehaviour
{
    public Camera mainCam;
    public Transform lookPoint;
    public Vector3 offset;
    public float fovAngle = 60f;
    public GameObject image;
    
    public bool firstTime = false;
    //private GameObject usingImage;
    public bool isOn;
    void Start()
    {

        mainCam = Camera.main;

        

    }

    // Update is called once per frame
    void Update()
    {
        if (firstTime)
        {

            isOn = true;

        }
        else
        {

            isOn = false;
        }

        if (isOn)
        {


            Vector3 directionToLookPoint = lookPoint.position - mainCam.transform.position;
            directionToLookPoint.Normalize();


            Vector3 cameraForward = mainCam.transform.forward;


            float angle = Vector3.Angle(cameraForward, directionToLookPoint);




            //player fov looking at lookpoint
            if (angle <= fovAngle)
            {

                Vector3 pos = mainCam.WorldToScreenPoint(lookPoint.position + offset);


                if (image.transform.position != pos)
                {
                    image.transform.position = pos;
                }

                //renable if previously was false
                if (!image.activeSelf)
                {
                    image.SetActive(true);
                }
            }
            else
            {

                if (image.activeSelf)
                {
                    image.SetActive(false);
                }
            }
        }
        else
        {
            image.SetActive(false);
        }
    }
}
