using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iconUIScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCam;
    public Transform lookPoint;
    public Vector3 offset;
    public float fovAngle = 60f;
    public GameObject image;
    private ticketGiverScript ticketGiverScript;
    private miniGameScript miniGameScript;
    private GameObject usingImage;
    public bool isOn;
    void Start()
    {
        
        mainCam = Camera.main;
        usingImage = image;
        ticketGiverScript =  FindAnyObjectByType<ticketGiverScript>();
        miniGameScript = FindAnyObjectByType<miniGameScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ticketGiverScript.canClaimTicket == true || miniGameScript.firstQuest == false)
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


                if (usingImage.transform.position != pos)
                {
                    usingImage.transform.position = pos;
                }

                //renable if previously was false
                if (!usingImage.activeSelf)
                {
                    usingImage.SetActive(true);
                }
            }
            else
            {

                if (usingImage.activeSelf)
                {
                    usingImage.SetActive(false);
                }
            }
        }
    }
}
