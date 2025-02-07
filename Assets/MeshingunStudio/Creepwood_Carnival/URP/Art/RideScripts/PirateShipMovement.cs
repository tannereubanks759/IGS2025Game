using UnityEngine;
using System.Collections;

public class PirateShipMovement : MonoBehaviour
{
    public float maxRotationAngle = 45f;      // Maximum rotation angle
    public float swingSpeed = 1f;             // Swing speed
    public float angleGrowthRate = 5f;        // Rate at which the rotation angle increases over time
    public float maxSwingDuration = 2f;       // Duration for which it will swing at the maximum angle
    public float startDelay = 1f;             // Delay before starting the motion
    public float endDelay = 1f;               // Delay after the motion completes

    private float currentMaxAngle = 0f;       // Current maximum angle, increasing over time
    private float timeElapsed = 0f;           // Tracks elapsed time for swinging motion
    private float swingTimeElapsed = 0f;      // Tracks time spent at the maximum angle
    private bool reachedMaxAngle = false;     // Checks if the maximum angle has been reached
    private bool isDecaying = false;          // Tracks if the motion is in the angle-reducing phase
    private bool isDelaying = true;           // Checks if the delay period is active

    void Start()
    {
        StartCoroutine(StartWithDelay());
    }

    void Update()
    {
        if (isDelaying) return; // Stop motion if in delay period

        if (!reachedMaxAngle)
        {
            // Gradually increase to the maximum rotation angle
            if (currentMaxAngle < maxRotationAngle)
            {
                currentMaxAngle += angleGrowthRate * Time.deltaTime;
                currentMaxAngle = Mathf.Min(currentMaxAngle, maxRotationAngle);  // Prevents exceeding maxRotationAngle
            }

            timeElapsed += Time.deltaTime * swingSpeed;
            float currentRotationAngle = Mathf.Sin(timeElapsed) * currentMaxAngle;
            transform.localRotation = Quaternion.Euler(currentRotationAngle, 0f, 0f);

            // Begin swing duration timer once max angle is reached
            if (currentMaxAngle >= maxRotationAngle)
            {
                reachedMaxAngle = true;
            }
        }
        else if (reachedMaxAngle && !isDecaying)
        {
            // Track time spent swinging at the maximum angle
            swingTimeElapsed += Time.deltaTime;

            if (swingTimeElapsed <= maxSwingDuration)
            {
                // Continue swinging
                timeElapsed += Time.deltaTime * swingSpeed;
                float currentRotationAngle = Mathf.Sin(timeElapsed) * currentMaxAngle;
                transform.localRotation = Quaternion.Euler(currentRotationAngle, 0f, 0f);
            }
            else
            {
                // Begin decreasing the rotation angle once swing duration ends
                isDecaying = true;
            }
        }
        else if (isDecaying)
        {
            // Gradually reduce the rotation angle back to zero
            currentMaxAngle -= angleGrowthRate * Time.deltaTime;
            currentMaxAngle = Mathf.Max(currentMaxAngle, 0f); // Prevents the angle from going below zero

            timeElapsed += Time.deltaTime * swingSpeed;
            float currentRotationAngle = Mathf.Sin(timeElapsed) * currentMaxAngle;
            transform.localRotation = Quaternion.Euler(currentRotationAngle, 0f, 0f);

            // Restart the loop when the rotation angle reaches zero
            if (currentMaxAngle <= 0.01f)
            {
                StartCoroutine(EndWithDelay());
            }
        }
    }

    private IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(startDelay);
        isDelaying = false;
    }

    private IEnumerator EndWithDelay()
    {
        isDelaying = true;
        yield return new WaitForSeconds(endDelay);

        // Reset loop variables and restart
        reachedMaxAngle = false;
        isDecaying = false;
        swingTimeElapsed = 0f;
        currentMaxAngle = 0f;
        timeElapsed = 0f;
        isDelaying = false;
    }
}
