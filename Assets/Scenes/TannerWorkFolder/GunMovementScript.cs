using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunMovementScript : MonoBehaviour
{
    float mouseX, mouseY, multiplier = .01f, xRotation, yRotation;
    public float senseX, senseY;

    public static bool isPaused = false;

    private void Start()
    {
        isPaused = false;
    }
    private void Update()
    {
        if (isPaused == false)
        {
            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");
            yRotation += mouseX * senseX * multiplier;
            xRotation -= mouseY * senseY * multiplier;

            xRotation = Mathf.Clamp(xRotation, -45f, 45f);

            this.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }


    }
}