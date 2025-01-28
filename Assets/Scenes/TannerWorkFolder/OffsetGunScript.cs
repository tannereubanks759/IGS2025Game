using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetGunScript : MonoBehaviour
{
    Vector3 vectOffset;
    public GameObject goFollow;
    float speed = 15.0f;


    // Start is called before the first frame update
    void Start()
    {
        vectOffset = transform.position - goFollow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(goFollow.activeSelf == false)
        {
            speed = 100;
        }
        else
        {
            speed = 10;
        }
        */

        transform.position = goFollow.transform.position + vectOffset;
        transform.rotation = Quaternion.Slerp(transform.rotation, goFollow.transform.rotation, speed * Time.deltaTime);

    }

}
