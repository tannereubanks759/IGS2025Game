using UnityEngine;

public class ClownRideMovement : MonoBehaviour
{
    // To change time of trap change acceleration and deceleration values


    public Transform center; // Center point
    public Transform childToRotate; // Child object that should rotate on its own axis
    public float maxRotationSpeed = 20f; // Maximum rotation speed
    public float acceleration = 5f; // Acceleration rate
    public float deceleration = 5f; // Deceleration rate
    public float maxSpeedDuration = 2f; // Time spent at maximum speed
    public float waitAtRestTime = 2f; // Rest time after deceleration
    public bool isActive = false;
    public clownTrap clownTrapRef;
    public float copyOfSpeed;
    public float minRiseHeight = 5f; // Minimum rise height
    public float maxRiseHeight = 15f; // Maximum rise height
    public float riseDuration = 2f; // Time taken to rise along the Y-axis
    public float descendDuration = 2f; // Time taken to descend along the Y-axis
    public bool enableYAxisMovement = false; // Controls movement along the Y-axis

    public float maxTiltAngle = 120f; // Maximum tilt angle along the Y-axis
    public float minTiltAngle = 60f; // Minimum tilt angle
    public float minSwingFrequency = 0.5f; // Minimum swing frequency
    public float maxSwingFrequency = 1.5f; // Maximum swing frequency
    public bool rotateOnOwnAxis = false; // Controls whether the child rotates on its own axis

    private float currentSpeed = 0f; // Current rotation speed
    private bool isAccelerating = true; // Acceleration state
    private bool isDecelerating = false; // Deceleration state
    private float maxSpeedTimeElapsed = 0f; // Time elapsed at maximum speed
    private float waitTimeElapsed = 0f; // Time elapsed during the rest period
    private bool isAtRest = false; // Rest state

    private float swingTimer = 0f; // Timer for swing motion
    private float randomTiltAngle; // Randomly assigned maximum tilt angle
    private float randomSwingFrequency; // Randomly assigned swing frequency

    private bool isRising = false; // State for rising along the Y-axis
    private float targetRiseHeight; // Target height for rising
    private float riseTimer = 0f; // Timer for rising
    private float descendTimer = 0f; // Timer for descending
    private float initialY; // Initial Y position

    void Start()
    {
        copyOfSpeed = maxRotationSpeed;
        initialY = transform.position.y; // Set initial Y position
        randomTiltAngle = Random.Range(minTiltAngle, maxTiltAngle);
        randomSwingFrequency = Random.Range(minSwingFrequency, maxSwingFrequency);

        if (enableYAxisMovement)
        {
            SetNewTargetHeight();
            isRising = true;
        }
    }

    void Update()
    {   if(isActive) 
        { 
        if (isAtRest)
        {
            waitTimeElapsed += Time.deltaTime;
            if (waitTimeElapsed >= waitAtRestTime)
            {
                isAtRest = false;
                isAccelerating = true;
            }
        }
        else if (isAccelerating)
        {
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed >= maxRotationSpeed)
            {
                currentSpeed = maxRotationSpeed;
                isAccelerating = false;
                maxSpeedTimeElapsed = 0f;
            }
        }
        else if (!isDecelerating && maxSpeedTimeElapsed < maxSpeedDuration)
        {
            maxSpeedTimeElapsed += Time.deltaTime;
        }
        else if (!isAccelerating && !isDecelerating)
        {
            currentSpeed -= deceleration * Time.deltaTime;
            if (currentSpeed <= 0)
            {
                isActive = false;
                clownTrapRef.paid = false;
                currentSpeed = 0;
                isDecelerating = false;
                isAtRest = true;
                waitTimeElapsed = 0f;
            }
        }

        if (enableYAxisMovement)
        {
            HandleYAxisMovement();
        }

        transform.RotateAround(center.position, Vector3.up, currentSpeed * Time.deltaTime);

        if (rotateOnOwnAxis && childToRotate != null)
        {
            swingTimer += Time.deltaTime * randomSwingFrequency;
            float speedRatio = currentSpeed / maxRotationSpeed;
            float dynamicTiltAngle = Mathf.Lerp(0, randomTiltAngle, speedRatio);
            float tiltAngle = Mathf.Sin(swingTimer) * dynamicTiltAngle;

            childToRotate.localRotation = Quaternion.Euler(childToRotate.localRotation.eulerAngles.x, tiltAngle, childToRotate.localRotation.eulerAngles.z);
        }
           /* if(copyOfSpeed>=6)
            {
                copyOfSpeed -= Time.deltaTime*25;
            }
            else
            {
                isActive = false;
            }*/
            
        }
    }

    private void HandleYAxisMovement()
    {
        if (isRising)
        {
            riseTimer += Time.deltaTime / riseDuration;
            float yOffset = Mathf.Lerp(0, targetRiseHeight, Mathf.SmoothStep(0, 1, riseTimer));
            transform.position = new Vector3(transform.position.x, initialY + yOffset, transform.position.z);

            if (riseTimer >= 1f)
            {
                riseTimer = 0f;
                isRising = false;
            }
        }
        else
        {
            descendTimer += Time.deltaTime / descendDuration;
            float yOffset = Mathf.Lerp(targetRiseHeight, 0, Mathf.SmoothStep(0, 1, descendTimer));
            transform.position = new Vector3(transform.position.x, initialY + yOffset, transform.position.z);

            if (descendTimer >= 1f)
            {
                descendTimer = 0f;
                isRising = true;
                SetNewTargetHeight();
            }
        }
    }

    private void SetNewTargetHeight()
    {
        targetRiseHeight = Random.Range(minRiseHeight, maxRiseHeight);
    }
}
